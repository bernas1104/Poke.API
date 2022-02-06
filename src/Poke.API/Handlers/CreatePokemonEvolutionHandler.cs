using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Queries.Requests;
using Poke.Core.Validations;
using Poke.Core.ValueObjects.Evolutions;

namespace Poke.API.Handlers
{
    public class CreatePokemonEvolutionHandler :
        IRequestHandler<CreatePokemonEvolutionRequest, Unit>
    {
        private readonly IEvolutionRepository _evolutionRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDomainNotification _domainNotification;
        private readonly IUnitOfWork _unitOfWork;
        private EvolutionValidation _validator;

        public CreatePokemonEvolutionHandler(
            IEvolutionRepository evolutionRepository,
            IMediator mediator,
            IMapper mapper,
            IDomainNotification domainNotification,
            IUnitOfWork unitOfWork
        )
        {
            _evolutionRepository = evolutionRepository;
            _mediator = mediator;
            _mapper = mapper;
            _domainNotification = domainNotification;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            CreatePokemonEvolutionRequest request,
            CancellationToken cancellationToken
        )
        {
            var unit = new Unit();

            _validator = new EvolutionValidation(
                default, EvolutionValidation.BOTH
            );

            var validationResult = _validator.Validate(
                _mapper.Map<PokemonEvolutionDTO>(request)
            );

            if (!validationResult.IsValid)
            {
                _domainNotification.AddValidationNotifications(
                    validationResult
                );
                return unit;
            }

            var pokemons = await _mediator.Send<List<Pokemon>>(
                new GetPokemonEvolutionPairRequest
                {
                    FromNumber = request.FromNumber.Value,
                    ToNumber = request.ToNumber.Value
                }
            );
            if (pokemons.Count != 2)
            {
                return unit;
            }

            var evolvesTo = Evolution.FromCreatePokemonEvolutionRequest(
                request, pokemons[1].Number
            );
            var evolvesFrom = PreEvolution.FromCreatePokemonEvolutionRequest(
                request, pokemons[0].Number
            );

            _evolutionRepository.Add(evolvesTo);
            _evolutionRepository.Add(evolvesFrom);
            _unitOfWork.Commit();

            return unit;
        }
    }
}

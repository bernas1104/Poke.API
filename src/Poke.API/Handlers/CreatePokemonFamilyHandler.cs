using System.Collections.Generic;
using System.Linq;
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
using Poke.Core.Notifications;
using Poke.Core.Validations;

namespace Poke.API.Handlers
{
    public class CreatePokemonFamilyHandler :
        IRequestHandler<CreatePokemonFamilyRequest, List<Pokemon>>
    {
        private readonly PokemonFamilyValidation _validator;
        private readonly IPokemonRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDomainNotification _domainNotification;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePokemonFamilyHandler(
            IPokemonRepository repository,
            IMapper mapper,
            IDomainNotification domainNotification,
            IUnitOfWork unitOfWork
        )
        {
            _validator = new PokemonFamilyValidation();

            _repository = repository;
            _mapper = mapper;
            _domainNotification = domainNotification;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Pokemon>> Handle(
            CreatePokemonFamilyRequest request,
            CancellationToken cancellationToken
        )
        {
            var pokemonFamily = new List<Pokemon>();

            var validationResult = _validator.Validate(
                _mapper.Map<List<PokemonDTO>>(request.Pokemons)
            );
            if (!validationResult.IsValid)
            {
                _domainNotification.AddValidationNotifications(
                    validationResult
                );
                return pokemonFamily;
            }

            var anyPokemonCreated = await _repository.PokemonsExistAsync(
                request.Pokemons.Select(x => x.Number)
            );
            if (anyPokemonCreated)
            {
                _domainNotification.AddNotification(
                    new NotificationMessage(
                        "Error",
                        "One or more of the pokemon being created already exist."
                    )
                );
                return pokemonFamily;
            }

            _unitOfWork.BeginTransaction();

            foreach (var pokemonRequest in request.Pokemons)
            {
                var pokemon = new Pokemon(pokemonRequest);
                _repository.Add(pokemon);
                pokemonFamily.Add(pokemon);
            }

            _unitOfWork.BeginCommit();
            _unitOfWork.Commit();

            return pokemonFamily;
        }
    }
}

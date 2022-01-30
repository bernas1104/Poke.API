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

namespace Poke.API.Handlers
{
    public class UpdatePokemonByNumberHandler :
        IRequestHandler<UpdatePokemonByNumberRequest, Unit>
    {
        private readonly IPokemonRepository _repository;
        private readonly IMediator _mediator;
        private readonly IDomainNotification _domainNotification;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PokemonValidation _validator;
        private readonly Unit _unit;

        public UpdatePokemonByNumberHandler(
            IPokemonRepository repository,
            IMediator mediator,
            IDomainNotification domainNotification,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _mediator = mediator;
            _domainNotification = domainNotification;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            _validator = new PokemonValidation();
            _unit = new Unit();
        }

        public async Task<Unit> Handle(
            UpdatePokemonByNumberRequest request,
            CancellationToken cancellationToken
        )
        {
            var validationResult = _validator.Validate(
                _mapper.Map<PokemonDTO>(request)
            );

            if(!validationResult.IsValid)
            {
                _domainNotification.AddValidationNotifications(validationResult);
                return _unit;
            }

            var pokemon = await _mediator.Send<Pokemon>(
                new GetPokemonByNumberRequest { Number = request.Number }
            );

            if (!pokemon.IsNull)
            {
                pokemon.UpdatePokemonData(request);

                _repository.Update(pokemon);
                _unitOfWork.Commit();
            }

            return _unit;
        }
    }
}

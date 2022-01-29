using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Notifications;
using Poke.Core.Validations;

namespace Poke.API.Handlers
{
    public class CreatePokemonHandler :
        IRequestHandler<CreatePokemonRequest, Pokemon>
    {
        private readonly CreatePokemonValidation _validator;
        private readonly IPokemonRepository _repository;
        private readonly IDomainNotification _domainNotification;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePokemonHandler(
            IPokemonRepository pokemonRepository,
            IDomainNotification domainNotification,
            IUnitOfWork unitOfWork
        )
        {
            _validator = new CreatePokemonValidation();

            _repository = pokemonRepository;
            _domainNotification = domainNotification;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pokemon> Handle(
            CreatePokemonRequest request,
            CancellationToken cancellationToken
        )
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                _domainNotification.AddValidationNotifications(
                    validationResult
                );
                return new NullPokemon();
            }

            var pokemonExists = await _repository.PokemonExistsAsync(
                request.Number
            );

            if (pokemonExists)
            {
                _domainNotification.AddNotification(
                    new NotificationMessage(
                        "Error",
                        $"Pokemon of number #{request.Number} already exists."
                    )
                );
                return new NullPokemon();
            }

            var pokemon = new Pokemon(request);

            _repository.Add(pokemon);
            _unitOfWork.Commit();

            return pokemon;
        }
    }
}

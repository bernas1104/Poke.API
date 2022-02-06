using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Notifications;
using Poke.Core.Queries.Requests;

namespace Poke.API.Handlers
{
    public class GetPokemonByNumberHandler :
        IRequestHandler<GetPokemonByNumberRequest, Pokemon>
    {
        private readonly IPokemonRepository _repository;
        private readonly IDomainNotification _domainNotification;

        public GetPokemonByNumberHandler(
            IPokemonRepository repository,
            IDomainNotification domainNotification
        )
        {
            _repository = repository;
            _domainNotification = domainNotification;
        }

        public async Task<Pokemon> Handle(
            GetPokemonByNumberRequest request,
            CancellationToken cancellationToken
        )
        {
            var pokemon = await _repository.GetByNumberAsync(request.Number);

            if (pokemon.IsNull)
            {
                _domainNotification.AddNotification(
                    new NotificationMessage(
                        "Error",
                        $"Pokemon of number #{request.Number} not found.",
                        HttpStatusCode.NotFound
                    )
                );
            }

            var evolutions = await _repository.GetAllPokemonEvolutionsByNumber(
                pokemon.Number
            );
            var preEvolutions = await _repository.GetAllPokemonPreEvolutionsByNumber(
                pokemon.Number
            );

            pokemon.SetPokemonEvolutions(evolutions.ToList());
            pokemon.SetPokemonPreEvolutions(preEvolutions.ToList());

            return pokemon;
        }
    }
}

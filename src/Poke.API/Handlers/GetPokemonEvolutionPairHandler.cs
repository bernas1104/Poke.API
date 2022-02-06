using System.Collections.Generic;
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
    public class GetPokemonEvolutionPairHandler :
        IRequestHandler<GetPokemonEvolutionPairRequest, List<Pokemon>>
    {
        private readonly IPokemonRepository _repository;
        private readonly IDomainNotification _domainNotification;

        public GetPokemonEvolutionPairHandler(
            IPokemonRepository repository,
            IDomainNotification domainNotification
        )
        {
            _repository = repository;
            _domainNotification = domainNotification;
        }

        public async Task<List<Pokemon>> Handle(
            GetPokemonEvolutionPairRequest request,
            CancellationToken cancellationToken
        )
        {
            var pokemons = await _repository.GetPokemonEvolutionPair(
                request.FromNumber,
                request.ToNumber
            );

            if (pokemons.Count < 2)
            {
                _domainNotification.AddNotification(
                    new NotificationMessage(
                        "Error",
                        "One or both pokemon not found.",
                        HttpStatusCode.NotFound
                    )
                );
                return new List<Pokemon>();
            }

            return pokemons;
        }
    }
}

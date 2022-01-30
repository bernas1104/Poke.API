using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Queries.Requests;

namespace Poke.API.Handlers
{
    public class GetAllPokemonHandler :
        IRequestHandler<GetAllPokemonRequest, List<Pokemon>>
    {
        private readonly IPokemonRepository _repository;

        public GetAllPokemonHandler(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pokemon>> Handle(
            GetAllPokemonRequest request,
            CancellationToken cancellationToken
        )
        {
            var pokemons = await _repository.GetAllAsync();
            return (await _repository.GetAllAsync()).ToList();
        }
    }
}

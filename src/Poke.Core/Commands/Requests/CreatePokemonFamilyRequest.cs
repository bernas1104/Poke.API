using System.Collections.Generic;
using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Commands.Requests
{
    public record CreatePokemonFamilyRequest : IRequest<List<Pokemon>>
    {
        public List<CreatePokemonRequest> Pokemons { get; init; }
    }
}

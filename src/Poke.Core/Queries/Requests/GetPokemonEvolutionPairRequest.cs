using System.Collections.Generic;
using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Queries.Requests
{
    public record GetPokemonEvolutionPairRequest : IRequest<List<Pokemon>>
    {
        public int FromNumber { get; init; }
        public int ToNumber { get; init; }
    }
}

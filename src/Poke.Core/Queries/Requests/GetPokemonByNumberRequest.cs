using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Queries.Requests
{
    public record GetPokemonByNumberRequest : IRequest<Pokemon>
    {
        public int Number { get; init; }
    }
}

using MediatR;

namespace Poke.Core.Commands.Requests
{
    public record DeletePokemonByNumberRequest : IRequest<Unit>
    {
        public int Number { get; init; }
    }
}

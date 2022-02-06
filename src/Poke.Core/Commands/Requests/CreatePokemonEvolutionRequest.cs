using System;
using System.Collections.Generic;
using MediatR;
using Poke.Core.ValueObjects;

namespace Poke.Core.Commands.Requests
{
    public record CreatePokemonEvolutionRequest : IRequest<List<AbstractEvolution>>
    {
        public int? ToNumber { get; init; }
        public int? FromNumber { get; init; }
        public int EvolutionType { get; init; }
        public int? PokemonEvolutionLevel { get; init; }
        public int? EvolutionStone { get; init; }
        public Guid? HeldItemId { get; init; }
    }
}

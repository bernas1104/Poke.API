using System;
using EnumsNET;
using Poke.Core.Enums;

namespace Poke.Core.Queries.Response
{
    public record EvolutionQueryResponse
    {
        public int FromNumber { get; init; }
        public int ToNumber { get; init; }
        public EvolutionType EvolutionType { get; init; }
        public string EvolutionTypeDescription {
            get => EvolutionType.AsString(EnumFormat.Description);
        }
        public int? PokemonEvolutionLevel { get; init; }
        public EvolutionStone? EvolutionStone { get; init; }
        public string EvolutionStoneDescription {
            get => EvolutionStone is null ?
                null :
                EvolutionStone.Value.AsString(EnumFormat.Description);
        }
        public Guid? HeldItemId { get; init; }
    }
}

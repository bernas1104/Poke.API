using System;

namespace Poke.Core.DTOs
{
    public record PokemonEvolutionDTO
    {
        public Guid Id { get; init; }
        public Guid FromId { get; init; }
        public Guid ToId { get; init; }
        public int EvolutionType { get; init; }
        public int? PokemonEvolutionLevel { get; init; }
        public int? EvolutionStone { get; init; }
        public string Discriminator { get; init; }
    }
}

using System;

namespace Poke.Core.DTOs
{
    public record PokemonEvolutionDTO
    {
        public Guid? Id { get; init; }
        public int FromNumber { get; set; }
        public int ToNumber { get; set; }
        public int EvolutionType { get; init; }
        public int? PokemonEvolutionLevel { get; init; }
        public int? EvolutionStone { get; init; }
        public Guid? HeldItemId { get; init; }
        public string Discriminator { get; init; }
    }
}

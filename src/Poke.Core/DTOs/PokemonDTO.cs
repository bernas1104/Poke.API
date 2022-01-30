using System;

namespace Poke.Core.DTOs
{
    public record PokemonDTO
    {
        public Guid Id { get; init; }
        public int Number { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public int FirstType { get; init; }
        public int SecondType { get; init; }
        public TrainingDTO Training { get; set; }
        public BaseStatsDTO BaseStats { get; set; }
    }
}

using Poke.Core.Enums;

namespace Poke.Application.Dtos.InputModels
{
    public record PokemonInputModel
    {
        public int Number { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public PokemonType FirstType { get; init; }
        public PokemonType? SecondType { get; init; }
        public TrainingInputModel Training { get; init; }
        public BaseStatsInputModel BaseStats { get; init; }
    }
}

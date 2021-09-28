using System;
using Poke.Core.Enums;

namespace Poke.Application.Dtos.ViewModels
{
    public record PokemonViewModel
    {
        public Guid Id { get; init; }
        public int Number { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public string FirstType { get; init; }
        public string SecondType { get; init; }
        public TrainingViewModel Training { get; init; }
        public BaseStatsViewModel BaseStats { get; init; }
    }
}

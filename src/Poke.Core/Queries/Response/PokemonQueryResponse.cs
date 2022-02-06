using System;
using System.Collections.Generic;

namespace Poke.Core.Queries.Response
{
    public record PokemonQueryResponse
    {
        public int Number { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public int FirstType { get; init; }
        public string FirstTypeDescription { get; init; }
        public int SecondType { get; init; }
        public string SecondTypeDescription { get; init; }
        public TrainingQueryResponse Training { get; init; }
        public BaseStatsQueryResponse BaseStats { get; init; }
        public List<EvolutionQueryResponse> Evolutions { get; init; }
        public List<EvolutionQueryResponse> PreEvolutions { get; init; }
    }
}

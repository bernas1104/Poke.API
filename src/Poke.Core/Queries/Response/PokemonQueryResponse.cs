using System;

namespace Poke.Core.Queries.Response
{
    public record PokemonQueryResponse
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string ImageUrl { get; set; }
        public int FirstType { get; set; }
        public string FirstTypeDescription { get; set; }
        public int SecondType { get; set; }
        public string SecondTypeDescription { get; set; }
        public TrainingQueryResponse Training { get; set; }
        public BaseStatsQueryResponse BaseStats { get; set; }
    }
}

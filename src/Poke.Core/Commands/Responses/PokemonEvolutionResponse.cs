namespace Poke.Core.Commands.Responses
{
    public record PokemonEvolutionResponse
    {
        public int EvolutionType { get; set; }
        public string EvolutionTypeDescription { get; set; }
        public int? PokemonEvolutionLevel { get; set; }
        public int? EvolutionStone { get; set; }
        public string EvolutionStoneDescription { get; set; }
        public PokemonBasicResponse EvolveTo { get; set; }
        public PokemonBasicResponse EvolveFrom { get; set; }
    }
}

namespace Poke.Core.Commands.Responses
{
    public record CreatePokemonResponse
    {
        public int Number { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public string ImageUrl { get; init; }
        public string FirstType { get; init; }
        public string SecondType { get; init; }
        public CreateBaseStatsResponse BaseStats { get; init; }
        public CreateTrainingResponse Training { get; init; }
    }
}

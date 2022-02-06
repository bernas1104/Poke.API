namespace Poke.Core.Commands.Responses
{
    public record PokemonBasicResponse
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }
}

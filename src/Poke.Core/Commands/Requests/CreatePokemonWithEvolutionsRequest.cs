using System.Collections.Generic;

namespace Poke.Core.Commands.Requests
{
    public record CreatePokemonWithEvolutionsRequest : CreatePokemonRequest
    {
        public List<CreatePokemonEvolutionRequest> Evolutions { get; init; }
        public List<CreatePokemonEvolutionRequest> PreEvolutions { get; init; }
    }
}

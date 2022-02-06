using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;

namespace Poke.Core.Interfaces.Repositories
{
    public interface IPokemonRepository : IEntityBaseRepository<Pokemon>
    {
        Task<IEnumerable<Pokemon>> GetAllAsync();
        Task<bool> PokemonExistsAsync(int number);
        Task<Pokemon> GetByNumberAsync(int number);
        Task<bool> PokemonsExistAsync(IEnumerable<int> pokemonNumbers);
        Task<IEnumerable<AbstractEvolution>> GetAllPokemonEvolutionsByNumber(
            int pokemonNumber
        );
        Task<IEnumerable<AbstractEvolution>> GetAllPokemonPreEvolutionsByNumber(
            int pokemonNumber
        );
        Task<List<Pokemon>> GetPokemonEvolutionPair(
            int pokemonEvolveFromNumber,
            int pokemonEvolveToNumber
        );
    }
}

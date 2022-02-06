using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Core.Entities;

namespace Poke.Core.Interfaces.Repositories
{
    public interface IPokemonRepository : IEntityBaseRepository<Pokemon>
    {
        Task<IEnumerable<Pokemon>> GetAllAsync();
        Task<bool> PokemonExistsAsync(int number);
        Task<Pokemon> GetByNumberAsync(int number);
        Task<bool> PokemonsExistAsync(IEnumerable<int> pokemonNumbers);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Core.Entities;

namespace Poke.Core.Interfaces.Repositories
{
    public interface IPokemonRepository : IEntityBaseRepository<Pokemon>
    {
        Task<IEnumerable<Pokemon>> GetAllAsync();
        Task<bool> PokemonExistsAsync(int number);
    }
}
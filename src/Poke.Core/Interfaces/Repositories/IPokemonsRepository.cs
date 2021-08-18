using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Core.Entities;

namespace Poke.Core.Interfaces.Repositories
{
    public interface IPokemonsRepository : IEntityBaseRepository<Pokemon>
    {
        Task<IEnumerable<Pokemon>> GetAllAsync();
    }
}

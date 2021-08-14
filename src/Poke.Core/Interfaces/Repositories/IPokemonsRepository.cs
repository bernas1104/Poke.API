using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Core.Entities;

namespace Poke.Core.Interfaces.Repositories
{
    public interface IPokemonsRepository
    {
        Task<IEnumerable<Pokemon>> GetAllAsync();
    }
}

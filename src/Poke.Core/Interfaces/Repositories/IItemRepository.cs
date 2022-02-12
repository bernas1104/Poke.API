using System.Threading.Tasks;
using Poke.Core.Entities;

namespace Poke.Core.Interfaces.Repositories
{
    public interface IItemRepository : IEntityBaseRepository<Item>
    {
        Task<Item> GetByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name);
    }
}

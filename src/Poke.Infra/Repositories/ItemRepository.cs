using System.Threading.Tasks;
using Dapper;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Infra.Context;

namespace Poke.Infra.Repositories
{
    public class ItemRepository : EntityBaseRepository<Item>, IItemRepository
    {
        private readonly DapperContext _dapperContext;

        public ItemRepository(
            EntityContext context,
            DapperContext dapperContext
        ) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var query = @$"
                SELECT COUNT(*)
                FROM dbo.item AS i WHERE i.name = '{name}'
            ";

            return await _dapperContext.DapperConnection.QuerySingleAsync<int>(
                query
            ) > 0;
        }
    }
}

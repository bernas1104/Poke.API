using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Interfaces.Repositories;
using Poke.Infra.Context;

namespace Poke.Infra.Repositories
{
    public class ItemRepository : EntityBaseRepository<Item>, IItemRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly string _selectStatements = $@"
            SELECT i.id,
                i.name,
                i.description,
                i.held_item,
                i.item_type
            FROM dbo.item AS i
        ";

        public ItemRepository(
            EntityContext context,
            DapperContext dapperContext
        ) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<Item>> GetAllAsync()
        {
            var items = new List<Item>();
            var itemDTOs = await _dapperContext.DapperConnection
                .QueryAsync<ItemDTO>(
                    _selectStatements
                );

            foreach (var itemDTO in itemDTOs)
            {
                items.Add(Item.FromDTO(itemDTO));
            }

            return items;
        }

        public async Task<Item> GetByNameAsync(string name)
        {
            var query = $@"
                {_selectStatements}
                WHERE i.name = '{name}'
            ";

            var queryResult = await _dapperContext.DapperConnection
                .QueryAsync<ItemDTO>(
                    query
                );

            var itemDTO = queryResult.FirstOrDefault();

            return itemDTO is null ?
                new NullItem() :
                Item.FromDTO(itemDTO);
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

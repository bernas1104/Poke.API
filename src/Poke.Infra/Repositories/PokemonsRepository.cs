using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Infra.Context;

namespace Poke.Infra.Repositories
{
    public class PokemonsRepository : EntityBaseRepository<Pokemon>,
        IPokemonsRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly string _selectQuery = $@"SELECT *
            FROM dbo.Pokemon";

        public PokemonsRepository(
            EntityContext context,
            DapperContext dapperContext
        ) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            var result = await _dapperContext
                .DapperConnection
                .QueryMultipleAsync(_selectQuery);

            return result.Read<Pokemon>();
        }
    }
}

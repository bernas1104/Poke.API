using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly string _selectQuery = $@"
            SELECT
                *
            FROM dbo.pokemon pokemon
        ";

        public PokemonsRepository(
            EntityContext context,
            DapperContext dapperContext
        ) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<bool> PokemonExists(int number)
        {
            var query = $@"SELECT COUNT(*) FROM dbo.pokemon WHERE number = @Number";

            var result = await _dapperContext.DapperConnection
                .QueryAsync<int>(
                    query,
                    new { Number = number }
                );

            var totalPokemon = result.FirstOrDefault();

            return totalPokemon >= 1;
        }

        public async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            return await _dapperContext
                .DapperConnection
                .QueryAsync<Pokemon>(_selectQuery);
        }
    }
}

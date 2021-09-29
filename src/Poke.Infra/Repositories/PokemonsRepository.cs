using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.ValueObjects;
using Poke.Infra.Context;
using Poke.Infra.DAO;

namespace Poke.Infra.Repositories
{
    public class PokemonsRepository : EntityBaseRepository<Pokemon>,
        IPokemonsRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly string _selectSimpleQuery = $@"
            SELECT
                p.id ,
                p.number ,
                p.name ,
                p.species ,
                p.height ,
                p.weight ,
                p.image_url as imageUrl ,
                p.first_type as firstType,
                p.second_type as seconType
            FROM dbo.pokemon p
        ";

        private readonly string _selectFullQuery = $@"
            SELECT
                p.id ,
                p.number ,
                p.name ,
                p.species ,
                p.height ,
                p.weight ,
                p.image_url as imageUrl,
                p.first_type as firstType,
                p.second_type as secondType,
                t.id,
                t.ev_yeld as evYeld,
                t.base_friendship as baseFriendship,
                t.growth_rate as growthRate,
                bs.id,
                bs.hitpoints ,
                bs.attack ,
                bs.defense ,
                bs.special_attack as specialAttack,
                bs.special_defense as specialDefense,
                bs.speed
            FROM dbo.pokemon p
        ";

        private readonly string _innerTrainingQuery = $@"
            INNER JOIN dbo.training t ON p.id = t.pokemon_id
        ";

        private readonly string _innerBaseStatsQuery = $@"
            INNER JOIN dbo.base_stats bs ON p.id = bs.pokemon_id
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
            var query = _selectFullQuery + _innerTrainingQuery +
                _innerBaseStatsQuery;

            return await _dapperContext
                .DapperConnection
                .QueryAsync<Pokemon, Training, BaseStatsDAO, Pokemon>(
                    query,
                    (pkmn, training, baseStatsDAO) =>
                    {
                        // TODO Estudar, em detalhes, ValueObjects
                        var baseStats = new BaseStats(
                            new Point(baseStatsDAO.HitPoints), new Point(baseStatsDAO.Attack),
                            new Point(baseStatsDAO.Defense), new Point(baseStatsDAO.SpecialAttack),
                            new Point(baseStatsDAO.SpecialDefense), new Point(baseStatsDAO.Speed)
                        );

                        pkmn.AddTrainingInformation(training);

                        pkmn.AddBaseStatsInformation(baseStats);

                        return pkmn;
                    },
                    splitOn: "id"
                );
        }
    }
}

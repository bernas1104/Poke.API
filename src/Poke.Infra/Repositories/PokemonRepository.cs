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
    public class PokemonRepository : EntityBaseRepository<Pokemon>,
        IPokemonRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly string _selectSimpleQuery = $@"
            SELECT
                p.id,
                p.number,
                p.name,
                p.species,
                p.height,
                p.weight,
                p.image_url AS imageUrl,
                p.first_type AS firstType,
                p.second_type AS secondType
            FROM dbo.pokemon AS p
        ";

        private readonly string _selectFullQuery = $@"
            SELECT
                p.id,
                p.number,
                p.name,
                p.species,
                p.height,
                p.weight,
                p.image_url AS imageUrl,
                p.first_type AS firstType,
                p.second_type AS secondType,
                bs.id,
                bs.hitpoints,
                bs.attack,
                bs.defense,
                bs.special_attack AS SpecialAttack,
                bs.special_defense AS SpecialDefense,
                bs.speed,
                bs.pokemon_id AS PokemonId,
                t.id,
                t.ev_yeld AS evYeld,
                t.base_friendship AS baseFriendship,
                t.growth_rate AS growthRate,
                t.pokemon_id AS pokemonId
            FROM dbo.pokemon AS p
        ";

        private readonly string _innerTrainingQuery = $@"
            INNER JOIN dbo.training AS t ON t.pokemon_id = p.id
        ";

        private readonly string _innerBaseStatsQuery = $@"
            INNER JOIN dbo.base_stats AS bs ON bs.pokemon_id = p.id
        ";

        public PokemonRepository(
            EntityContext context,
            DapperContext dapperContext
        ) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<bool> PokemonExistsAsync(int number)
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

            var pokemonDto = await _dapperContext.DapperConnection
                .QueryAsync<PokemonDTO, BaseStatsDTO, TrainingDTO, PokemonDTO>(
                    query,
                    map: MapPokemon,
                    splitOn: "id"
                );

            var pokemons = new List<Pokemon>();
            foreach (var dto in pokemonDto)
            {
                pokemons.Add(Pokemon.FromPokemonDTO(dto));
            }

            return pokemons;
        }

        public async Task<Pokemon> GetByNumberAsync(int number)
        {
            var query = _selectFullQuery + _innerTrainingQuery +
                _innerBaseStatsQuery + $" WHERE p.number = {number}";

            var queryResult = await _dapperContext.DapperConnection
                .QueryAsync<PokemonDTO, BaseStatsDTO, TrainingDTO, PokemonDTO>(
                    query,
                    map: MapPokemon,
                    splitOn: "id"
                );

            var pokemonDto = queryResult.FirstOrDefault();

            return pokemonDto is not null ?
                Pokemon.FromPokemonDTO(pokemonDto) :
                new NullPokemon();
        }

        private PokemonDTO MapPokemon(
            PokemonDTO pkmnDto,
            BaseStatsDTO baseDto,
            TrainingDTO trainingDto
        )
        {
            pkmnDto.Training = trainingDto;
            pkmnDto.BaseStats = baseDto;

            return pkmnDto;
        }
    }
}

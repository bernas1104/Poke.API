using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.ValueObjects;
using Poke.Core.ValueObjects.Evolutions;
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
                bs.special_attack AS specialAttack,
                bs.special_defense AS specialDefense,
                bs.speed,
                bs.pokemon_number AS pokemonNumber,
                t.id,
                t.ev_yeld AS evYeld,
                t.base_friendship AS baseFriendship,
                t.growth_rate AS growthRate,
                t.pokemon_number AS pokemonNumber
            FROM dbo.pokemon AS p
        ";

        private readonly string _innerTrainingQuery = $@"
            INNER JOIN dbo.training AS t ON t.pokemon_number = p.number
        ";

        private readonly string _innerBaseStatsQuery = $@"
            INNER JOIN dbo.base_stats AS bs ON bs.pokemon_number = p.number
        ";

        private readonly string _evolutionSelectQuery = $@"
            SELECT e.id,
                e.from_number AS fromNumber,
                e.to_number AS toNumber,
                e.evolution_type AS evolutionType,
                e.pokemon_evolution_level AS pokemonEvolutionLevel,
                e.evolution_stone AS evolutionStone,
                e.held_item_id AS heldItemId
                FROM dbo.evolution AS e
            INNER JOIN dbo.pokemon AS p ON e.@Number = p.number
            WHERE p.number = @PokemonNumber AND e.discriminator = 'Evolution'
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

            return pokemons.OrderBy(x => x.Number);
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

        public async Task<bool> PokemonsExistAsync(
            IEnumerable<int> pokemonNumbers
        )
        {
            var query = new StringBuilder(
                $@"
                    SELECT COUNT(*)
                    FROM dbo.pokemon AS p WHERE p.number = {pokemonNumbers.First()}
                "
            );

            foreach (var pokemonNumber in pokemonNumbers.Skip(1))
            {
                query.Append(@$" OR p.number = {pokemonNumber}");
            }

            var results = await _dapperContext.DapperConnection.QueryAsync<int>(
                query.ToString()
            );

            var count = results.FirstOrDefault();
            return count >= 1;
        }

        public async Task<IEnumerable<AbstractEvolution>> GetAllPokemonEvolutionsByNumber(
            int pokemonNumber
        )
        {
            var query = _evolutionSelectQuery.Replace("e.@Number", "e.from_number");

            return await _dapperContext.DapperConnection.QueryAsync<Evolution>(
                query,
                new { PokemonNumber = pokemonNumber}
            );
        }

        public async Task<IEnumerable<AbstractEvolution>> GetAllPokemonPreEvolutionsByNumber(
            int pokemonNumber
        )
        {
            var query = _evolutionSelectQuery.Replace("e.@Number", "e.to_number");

            return await _dapperContext.DapperConnection.QueryAsync<PreEvolution>(
                query,
                new { PokemonNumber = pokemonNumber}
            );
        }

        public async Task<List<Pokemon>> GetPokemonEvolutionPair(
            int pokemonEvolveFromNumber,
            int pokemonEvolveToNumber
        )
        {
            var query = _selectFullQuery + _innerTrainingQuery +
                _innerBaseStatsQuery +
                @$"
                    WHERE p.number = {pokemonEvolveFromNumber}
                    OR p.number = {pokemonEvolveToNumber}
                ";

            var queryResult = await _dapperContext.DapperConnection
                .QueryAsync<PokemonDTO, BaseStatsDTO, TrainingDTO, PokemonDTO>(
                    query,
                    map: MapPokemon,
                    splitOn: "id"
                );

            return queryResult.Select(x => Pokemon.FromPokemonDTO(x)).AsList();
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

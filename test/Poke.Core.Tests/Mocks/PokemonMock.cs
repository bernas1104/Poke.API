using System;
using AutoBogus;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Enums;
using Poke.Core.Queries.Requests;
using Poke.Core.Queries.Response;

namespace Poke.Core.Tests.Mocks
{
    public static class PokemonMock
    {
        public static Faker<Pokemon> PokemonFaker =>
            new AutoFaker<Pokemon>()
                .CustomInstantiator(
                    f =>
                    {
                        var pkmnGuid = Guid.NewGuid();

                        var undefined = f.Random.Bool();

                        return new Pokemon(
                            pkmnGuid, f.Random.Int(1, 151), f.Person.FirstName,
                            f.Random.Word(), f.Random.Double(1, 10),
                            f.Random.Double(1, 500), f.Internet.Url(),
                            (PokemonType)f.Random.Int(1, 8),
                            undefined ?
                                PokemonType.Undefined :
                                (PokemonType)f.Random.Int(9, 15)
                        );
                    }
                );

        public static Faker<CreatePokemonRequest> CreatePokemonRequestFaker =>
            new Faker<CreatePokemonRequest>()
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151))
                .RuleFor(x => x.Name, f => f.Random.Word())
                .RuleFor(x => x.Species, f => f.Random.Word())
                .RuleFor(x => x.Height, f => f.Random.Double(0.01, 100))
                .RuleFor(x => x.Weight, f => f.Random.Double(0.01, 100))
                .RuleFor(x => x.ImageUrl, f => f.Internet.Url())
                .RuleFor(x => x.FirstType, f => f.Random.Int(1, 15))
                .RuleFor(x => x.SecondType, f => f.Random.Int(0, 15))
                .RuleFor(x => x.BaseStats, BaseStatsMock.BaseStatsRequestFaker)
                .RuleFor(
                    x => x.Training,
                    TrainingMock.TrainingRequestFaker
                )
                .RuleFor(
                    x => x.Evolutions,
                    f => EvolutionMock.CreatePokemonEvolutionRequestFaker
                        .Generate(f.Random.Int(0, 1))
                )
                .RuleFor(
                    x => x.PreEvolutions,
                    f => EvolutionMock.CreatePokemonPreEvolutionRequestFaker
                        .Generate(f.Random.Int(0, 1))
                );

        public static Faker<PokemonDTO> PokemonDTOFaker =>
            new Faker<PokemonDTO>()
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151))
                .RuleFor(x => x.Name, f => f.Random.Word())
                .RuleFor(x => x.Species, f => f.Random.Word())
                .RuleFor(x => x.Height, f => f.Random.Double(0.01, 100))
                .RuleFor(x => x.Weight, f => f.Random.Double(0.01, 100))
                .RuleFor(x => x.ImageUrl, f => f.Internet.Url())
                .RuleFor(x => x.FirstType, f => f.Random.Int(1, 15))
                .RuleFor(x => x.SecondType, f => f.Random.Int(0, 15))
                .RuleFor(x => x.BaseStats, BaseStatsMock.BaseStatsDTOFaker)
                .RuleFor(x => x.Training, TrainingMock.TrainingDTOFaker);

        public static Faker<PokemonQueryResponse> PokemonQueryResponseFaker =>
            new Faker<PokemonQueryResponse>();

        public static Faker<GetPokemonByNumberRequest> GetPokemonByNumberRequestFaker =>
            new Faker<GetPokemonByNumberRequest>()
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151));

        public static Faker<DeletePokemonByNumberRequest> DeletePokemonByNumberRequestFaker =>
            new Faker<DeletePokemonByNumberRequest>()
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151));

        public static Faker<UpdatePokemonByNumberRequest> UpdatePokemonByNumberRequestFaker =>
            new Faker<UpdatePokemonByNumberRequest>()
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151))
                .RuleFor(x => x.Name, f => f.Random.Word())
                .RuleFor(x => x.Species, f => f.Random.Word())
                .RuleFor(x => x.Height, f => f.Random.Double(0.01, 100))
                .RuleFor(x => x.Weight, f => f.Random.Double(0.01, 100))
                .RuleFor(x => x.ImageUrl, f => f.Internet.Url())
                .RuleFor(x => x.FirstType, f => f.Random.Int(1, 15))
                .RuleFor(x => x.SecondType, f => f.Random.Int(0, 15))
                .RuleFor(x => x.BaseStats, BaseStatsMock.BaseStatsRequestFaker)
                .RuleFor(
                    x => x.Training,
                    TrainingMock.TrainingRequestFaker
                );

        public static Faker<CreatePokemonFamilyRequest> CreatePokemonFamilyRequestFaker =>
            new Faker<CreatePokemonFamilyRequest>()
                .RuleFor(
                    x => x.Pokemons,
                    f => CreatePokemonRequestFaker.Generate(f.Random.Int(1, 3))
                );
    }
}

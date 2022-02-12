using System;
using AutoBogus;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Entities;
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
                        var pokemonDto = new PokemonDTO
                        {
                            Id = Guid.NewGuid(),
                            Number = f.Random.Int(1, 151),
                            Name = f.Person.FirstName,
                            Species = f.Random.Word(),
                            Height = f.Random.Double(1, 10),
                            Weight = f.Random.Double(1, 500),
                            ImageUrl = f.Internet.Url(),
                            FirstType = f.Random.Int(1, 8),
                            SecondType = undefined ? 0 : f.Random.Int(9, 15),
                            BaseStats = BaseStatsMock.BaseStatsDTOFaker,
                            Training = TrainingMock.TrainingDTOFaker,
                            Evolutions = EvolutionMock.PokemonEvolutionDtoFaker
                                .Generate(f.Random.Int(0, 2)),
                            PreEvolutions = EvolutionMock.PokemonPreEvolutionDtoFaker
                                .Generate(f.Random.Int(0, 2))
                        };

                        return Pokemon.FromPokemonDTO(pokemonDto);
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

        public static Faker<GetPokemonEvolutionPairRequest> GetPokemonEvolutionPairRequestFaker =>
            new Faker<GetPokemonEvolutionPairRequest>()
                .RuleFor(x => x.FromNumber, f => f.Random.Int(1, 74))
                .RuleFor(x => x.ToNumber, f => f.Random.Int(75, 151));
    }
}

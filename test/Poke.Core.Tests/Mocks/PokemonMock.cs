using System;
using AutoBogus;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Enums;

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
                .RuleFor(x => x.BaseStats, BaseStatsMock.CreateBaseStatsFaker)
                .RuleFor(
                    x => x.Training,
                    TrainingMock.CreateTrainingRequestFaker
                );
    }
}

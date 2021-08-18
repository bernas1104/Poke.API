using System;
using AutoBogus;
using Bogus;
using Poke.Core.Entities;
using Poke.Core.Entities.Enumerations;
using Poke.Core.ValueObjects;

namespace Poke.Core.Tests.Mocks
{
    public static class PokemonMock
    {
        public static Faker<Pokemon> PokemonFaker =>
            new AutoFaker<Pokemon>()
                .RuleFor(x => x.Id, () => Guid.NewGuid())
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151))
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Species, f => f.Random.Word())
                .RuleFor(x => x.Height, f => f.Random.Double(1, 10))
                .RuleFor(x => x.Weight, f => f.Random.Double(1, 500))
                .RuleFor(x => x.ImageUrl, f => f.Internet.Url())
                .RuleFor(x => x.FirstType, f => (PokemonType)f.Random.Int(0, 14))
                .RuleFor(
                    x => x.SecondType,
                    f =>
                    {
                        var rdn = f.Random.Int(-1, 14);

                        return rdn < 0 ? null : (PokemonType)rdn;
                    }
                )
                .RuleFor(x => x.Traning, (_, pkmn) => TrainingFaker(pkmn.Id))
                .RuleFor(x => x.BaseStats, (_, pkmn) => BaseStatsFaker(pkmn.Id));

        public static Faker<Training> TrainingFaker(Guid pokemonId) =>
            new AutoFaker<Training>()
                .RuleFor(x => x.Id, f => f.UniqueIndex)
                .RuleFor(x => x.EVYeld, f => f.Random.Int(1, 3))
                .RuleFor(x => x.CatchRate, f => f.Random.Double(0, 100))
                .RuleFor(x => x.BaseFriendship, f => f.Random.Int(0, 140))
                .RuleFor(x => x.BaseExperience, f => f.Random.Int(1, 500))
                .RuleFor(x => x.GrowthRate, f => (GrowthRate)f.Random.Int(0, 5))
                .RuleFor(x => x.PokemonId, () => pokemonId);

        public static Faker<BaseStats> BaseStatsFaker(Guid pokemonId) =>
            new AutoFaker<BaseStats>()
                .RuleFor(x => x.Id, f => f.UniqueIndex)
                .RuleFor(x => x.HitPoints, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Attack, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Defense, f => f.Random.Int(1, 255))
                .RuleFor(x => x.SpecialAttack, f => f.Random.Int(1, 255))
                .RuleFor(x => x.SpecialDefense, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Speed, f => f.Random.Int(1, 255))
                .RuleFor(x => x.PokemonId, () => pokemonId);
    }
}

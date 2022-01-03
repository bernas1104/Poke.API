using System;
using AutoBogus;
using Bogus;
using Poke.Core.Entities;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;

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
                            f.Random.Word(), f.Random.Double(1, 10), f.Random.Double(1, 500),
                            f.Internet.Url(), (PokemonType)f.Random.Int(1, 8),
                            undefined ?
                                PokemonType.Undefined :
                                (PokemonType)f.Random.Int(9, 15)
                        );
                    }
                );

        public static Faker<Training> TrainingFaker(Guid pokemonId) =>
            new AutoFaker<Training>()
                .CustomInstantiator(
                    f =>
                    {
                        return new Training(
                            Guid.NewGuid(), f.Random.Int(1, 3), f.Random.Int(50, 140),
                            (GrowthRate)f.Random.Int(0, 5), pokemonId
                        );
                    }
                );

        public static Faker<BaseStats> BaseStatsFaker(Guid pokemonId) =>
            new AutoFaker<BaseStats>()
                .CustomInstantiator(
                    f =>
                    {
                        var hitPoints = new Point(f.Random.Int(1, 250));

                        var attack = new Point(f.Random.Int(1, 250));

                        var defense = new Point(f.Random.Int(1, 250));

                        var specialAttack = new Point(f.Random.Int(1, 250));

                        var specialDefense = new Point(f.Random.Int(1, 250));

                        var speed = new Point(f.Random.Int(1, 250));

                        return new BaseStats(
                            Guid.NewGuid(), hitPoints, attack, defense,
                            specialAttack, specialDefense, speed, pokemonId
                        );
                    }
                );
    }
}

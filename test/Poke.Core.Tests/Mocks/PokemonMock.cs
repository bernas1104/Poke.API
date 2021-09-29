using System;
using AutoBogus;
using Bogus;
using Poke.Application.Dtos.InputModels;
using Poke.Application.Dtos.ViewModels;
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

        public static Faker<PokemonInputModel> InvalidPokemonInputModelFaker =>
            new AutoFaker<PokemonInputModel>()
                .RuleFor(x => x.Number, () => 0)
                .RuleFor(x => x.Name, () => "")
                .RuleFor(x => x.Species, () => "")
                .RuleFor(x => x.Height, () => 0)
                .RuleFor(x => x.Weight, () => 0)
                .RuleFor(x => x.ImageUrl, () => "")
                .RuleFor(x => x.FirstType, () => (PokemonType)0)
                .RuleFor(x => x.SecondType, () => (PokemonType)0)
                .RuleFor(x => x.Training, () => InvalidTrainingFaker)
                .RuleFor(x => x.BaseStats, () => InvalidBaseStatsFaker);

        public static Faker<TrainingInputModel> InvalidTrainingFaker =>
            new AutoFaker<TrainingInputModel>()
                .RuleFor(x => x.EVYeld, () => 0)
                .RuleFor(x => x.BaseFriendship, () => 40)
                .RuleFor(x => x.GrowthRate, () => (GrowthRate)10);

        public static Faker<BaseStatsInputModel> InvalidBaseStatsFaker =>
            new AutoFaker<BaseStatsInputModel>()
                .RuleFor(x => x.HitPoints, () => 0)
                .RuleFor(x => x.Attack, () => 0)
                .RuleFor(x => x.Defense, () => 0)
                .RuleFor(x => x.SpecialAttack, () => 0)
                .RuleFor(x => x.SpecialDefense, () => 0)
                .RuleFor(x => x.Speed, () => 0);

        public static Faker<PokemonInputModel> PokemonInputModelFaker =>
            new AutoFaker<PokemonInputModel>()
                .RuleFor(x => x.Number, f => f.Random.Int(1, 151))
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Species, f => f.Random.Word())
                .RuleFor(x => x.Height, f => f.Random.Double(1, 100))
                .RuleFor(x => x.Weight, f => f.Random.Double(1, 100))
                .RuleFor(x => x.ImageUrl, f => f.Internet.Url())
                .RuleFor(x => x.FirstType, f => (PokemonType)f.Random.Int(1, 8))
                .RuleFor(
                    x => x.SecondType,
                    f => f.Random.Bool() ?
                        (PokemonType)f.Random.Int(9, 15) :
                        PokemonType.Undefined
                )
                .RuleFor(x => x.BaseStats, () => BaseStatsInputModelFaker)
                .RuleFor(x => x.Training, () => TrainingInputModelFaker);

        public static Faker<BaseStatsInputModel> BaseStatsInputModelFaker =>
            new AutoFaker<BaseStatsInputModel>()
                .RuleFor(x => x.HitPoints, f => f.Random.Int(30, 90))
                .RuleFor(x => x.Attack, f => f.Random.Int(30, 90))
                .RuleFor(x => x.Defense, f => f.Random.Int(30, 90))
                .RuleFor(x => x.SpecialAttack, f => f.Random.Int(30, 90))
                .RuleFor(x => x.SpecialDefense, f => f.Random.Int(30, 90))
                .RuleFor(x => x.Speed, f => f.Random.Int(30, 90));

        public static Faker<TrainingInputModel> TrainingInputModelFaker =>
            new AutoFaker<TrainingInputModel>()
                .RuleFor(x => x.EVYeld, f => f.Random.Int(1, 3))
                .RuleFor(x => x.BaseFriendship, f => f.Random.Int(50, 140))
                .RuleFor(x => x.GrowthRate, f => (GrowthRate)f.Random.Int(0, 5));

        public static Faker<PokemonViewModel> PokemonViewModelFaker =>
            new AutoFaker<PokemonViewModel>()
                .RuleFor(x => x.BaseStats, () => BaseStatsViewModelFaker)
                .RuleFor(x => x.Training, () => TrainingViewModelFaker);

        public static Faker<BaseStatsViewModel> BaseStatsViewModelFaker =>
            new AutoFaker<BaseStatsViewModel>();

        public static Faker<TrainingViewModel> TrainingViewModelFaker =>
            new AutoFaker<TrainingViewModel>();
    }
}

using System;
using AutoBogus;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Models;
using Poke.Core.ValueObjects;

namespace Poke.Core.Tests.Mocks
{
    public static class BaseStatsMock
    {
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
                            hitPoints, attack, defense,
                            specialAttack, specialDefense, speed, pokemonId
                        );
                    }
                );

        public static Faker<BaseStatsRequest> BaseStatsRequestFaker =>
            new Faker<BaseStatsRequest>()
                .RuleFor(x => x.HitPoints, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Attack, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Defense, f => f.Random.Int(1, 255))
                .RuleFor(x => x.SpecialAttack, f => f.Random.Int(1, 255))
                .RuleFor(x => x.SpecialDefense, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Speed, f => f.Random.Int(1, 255));

        public static Faker<BaseStatsDTO> BaseStatsDTOFaker =>
            new Faker<BaseStatsDTO>()
                .RuleFor(x => x.HitPoints, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Attack, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Defense, f => f.Random.Int(1, 255))
                .RuleFor(x => x.SpecialAttack, f => f.Random.Int(1, 255))
                .RuleFor(x => x.SpecialDefense, f => f.Random.Int(1, 255))
                .RuleFor(x => x.Speed, f => f.Random.Int(1, 255));
    }
}

using System;
using AutoBogus;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;

namespace Poke.Core.Tests.Mocks
{
    public static class TrainingMock
    {
        public static Faker<Training> TrainingFaker(int pokemonNumber) =>
            new AutoFaker<Training>()
                .CustomInstantiator(
                    f =>
                    {
                        return new Training(
                            f.Random.Int(1, 3), f.Random.Int(50, 140),
                            (GrowthRate)f.Random.Int(0, 5), pokemonNumber
                        );
                    }
                );

        public static Faker<TrainingRequest> TrainingRequestFaker =>
            new Faker<TrainingRequest>()
                .RuleFor(x => x.EVYeld, f => f.Random.Int(1, 3))
                .RuleFor(x => x.BaseFriendship, f => f.Random.Int(0, 140))
                .RuleFor(x => x.GrowthRate, f => f.Random.Int(0, 5));

        public static Faker<TrainingDTO> TrainingDTOFaker =>
            new Faker<TrainingDTO>()
                .RuleFor(x => x.EVYeld, f => f.Random.Int(1, 3))
                .RuleFor(x => x.BaseFriendship, f => f.Random.Int(0, 140))
                .RuleFor(x => x.GrowthRate, f => f.Random.Int(0, 5));
    }
}

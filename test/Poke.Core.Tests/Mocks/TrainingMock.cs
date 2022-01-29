using System;
using AutoBogus;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;

namespace Poke.Core.Tests.Mocks
{
    public static class TrainingMock
    {
        public static Faker<Training> TrainingFaker(Guid pokemonId) =>
            new AutoFaker<Training>()
                .CustomInstantiator(
                    f =>
                    {
                        return new Training(
                            f.Random.Int(1, 3), f.Random.Int(50, 140),
                            (GrowthRate)f.Random.Int(0, 5), pokemonId
                        );
                    }
                );

        public static Faker<CreateTrainingRequest> CreateTrainingRequestFaker =>
            new Faker<CreateTrainingRequest>()
                .RuleFor(x => x.EVYeld, f => f.Random.Int(1, 3))
                .RuleFor(x => x.BaseFriendship, f => f.Random.Int(0, 140))
                .RuleFor(x => x.GrowthRate, f => f.Random.Int(0, 5));
    }
}

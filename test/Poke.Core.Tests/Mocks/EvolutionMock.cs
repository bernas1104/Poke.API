using System;
using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;
using Poke.Core.ValueObjects.Evolutions;

namespace Poke.Core.Tests.Mocks
{
    public static class EvolutionMock
    {
        public static Faker<PokemonEvolutionDTO> PokemonEvolutionDtoFaker =>
            new Faker<PokemonEvolutionDTO>()
                .RuleFor(x => x.ToNumber, f => f.Random.Int(75, 151))
                .RuleFor(x => x.PokemonEvolutionLevel, f => f.Random.Int(2, 100))
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.Level)
                .RuleFor(x => x.EvolutionStone, f => f.Random.Int(0, 9))
                .RuleFor(x => x.HeldItemName, f => f.Random.Word())
                .RuleFor(x => x.HeldItemId, Guid.NewGuid());

        public static Faker<PokemonEvolutionDTO> PokemonPreEvolutionDtoFaker =>
            new Faker<PokemonEvolutionDTO>()
                .RuleFor(x => x.FromNumber, f => f.Random.Int(75, 151))
                .RuleFor(x => x.PokemonEvolutionLevel, f => f.Random.Int(2, 100))
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.Level)
                .RuleFor(x => x.EvolutionStone, f => f.Random.Int(0, 9))
                .RuleFor(x => x.HeldItemName, f => f.Random.Word())
                .RuleFor(x => x.HeldItemId, Guid.NewGuid());

        public static Faker<CreatePokemonEvolutionRequest> CreatePokemonEvolutionRequestFaker =>
            new Faker<CreatePokemonEvolutionRequest>()
                .RuleFor(x => x.ToNumber, f => f.Random.Int(75, 151))
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.Level)
                .RuleFor(x => x.PokemonEvolutionLevel, f => f.Random.Int(2, 100))
                .RuleFor(x => x.EvolutionStone, f => f.Random.Int(0, 9))
                .RuleFor(x => x.HeldItemName, f => f.Random.Word());

        public static Faker<CreatePokemonEvolutionRequest> CreatePokemonPreEvolutionRequestFaker =>
            new Faker<CreatePokemonEvolutionRequest>()
                .RuleFor(x => x.FromNumber, f => f.Random.Int(75, 151))
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.Level)
                .RuleFor(x => x.PokemonEvolutionLevel, f => f.Random.Int(2, 100))
                .RuleFor(x => x.EvolutionStone, f => f.Random.Int(0, 9))
                .RuleFor(x => x.HeldItemName, f => f.Random.Word());

        public static Faker<AbstractEvolution> AbstractEvolutionFaker(
            bool evolution
        ) => new Faker<AbstractEvolution>()
            .CustomInstantiator(f => {
                if (evolution)
                {
                    return new Faker<Evolution>();
                }

                return new Faker<PreEvolution>();
            });
    }
}

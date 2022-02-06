using System;
using Bogus;
using FluentAssertions;
using Poke.Core.Enums;
using Poke.Core.Tests.Mocks;
using Poke.Core.Validations;
using Xunit;

namespace Poke.Unit.Tests.Core.Validations
{
    public class EvolutionValidationTest
    {
        private EvolutionValidation _validator;
        private readonly Faker _faker;

        public EvolutionValidationTest()
        {
            _faker = new Faker();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(152)]
        public void Should_Invalidate_PreEvolution_If_Evolves_From_Invalid_Pokemon_Number(
            int fromNumber
        )
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), EvolutionValidation.PRE_EVOLUTION
            );

            var dto = EvolutionMock.PokemonPreEvolutionDtoFaker
                .RuleFor(x => x.FromNumber, fromNumber);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(152)]
        public void Should_Invalidate_Evolution_If_Evolves_To_Invalid_Pokemon_Number(
            int toNumber
        )
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), EvolutionValidation.EVOLUTION
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, toNumber);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(0, 152)]
        [InlineData(152, 0)]
        public void Should_Invalidate_Evolution_If_Any_Pokemon_Number_Invalid(
            int fromNumber, int toNumber
        )
        {
            // Arrange
            _validator = new EvolutionValidation(
                default, EvolutionValidation.BOTH
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.FromNumber, fromNumber)
                .RuleFor(x => x.ToNumber, toNumber);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void Should_Invalidate_Evolution_If_Evolves_FromTo_Pokemon_With_Same_Number()
        {
            // Arrange
            var pokemonNumber = _faker.Random.Int(1, 151);

            _validator = new EvolutionValidation(
                pokemonNumber, _faker.Random.Int(1, 2)
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, pokemonNumber)
                .RuleFor(x => x.FromNumber, pokemonNumber);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void Should_Invalidate_Evolution_If_Evolves_Both_Pokemon_With_Same_Number()
        {
            // Arrange
            var pokemonNumber = _faker.Random.Int(1, 151);

            _validator = new EvolutionValidation(
                default, EvolutionValidation.BOTH
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, pokemonNumber)
                .RuleFor(x => x.FromNumber, pokemonNumber);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public void Should_Invalidate_Evolution_If_EvolutionType_Invalid(
            int evolutionType
        )
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), _faker.Random.Int(1, 2)
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.FromNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.EvolutionType, evolutionType);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(101)]
        public void Should_Invalidate_Evolution_If_EvolutionLevel_Invalid(
            int evolutionLevel
        )
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), _faker.Random.Int(1, 2)
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.FromNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.PokemonEvolutionLevel, evolutionLevel);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void Should_Invalidate_Evolution_If_EvolutionStone_Invalid(
            int evolutionStone
        )
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), _faker.Random.Int(1, 2)
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.FromNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.Stone)
                .RuleFor(x => x.EvolutionStone, evolutionStone);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void Should_Invalidate_Evolution_If_HeldItemId_Invalid()
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), _faker.Random.Int(1, 2)
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.FromNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.TradeWithItem)
                .RuleFor(x => x.HeldItemId, () => null);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Should_Validate_PokemonEvolution(int evolutionType)
        {
            // Arrange
            _validator = new EvolutionValidation(
                _faker.Random.Int(1, 74), _faker.Random.Int(1, 2)
            );

            var dto = EvolutionMock.PokemonEvolutionDtoFaker
                .RuleFor(x => x.ToNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.FromNumber, _faker.Random.Int(75, 151))
                .RuleFor(x => x.EvolutionType, evolutionType);

            // Act
            var validationResult = _validator.Validate(dto);

            // Assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}

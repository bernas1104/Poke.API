using FluentAssertions;
using Poke.Core.Tests.Mocks;
using Poke.Core.Validations;
using Xunit;

namespace Poke.Unit.Tests.Core.Validations
{
    public class CreatePokemonValidationTest
    {
        private readonly CreatePokemonValidation _validator;

        public CreatePokemonValidationTest()
        {
            _validator = new CreatePokemonValidation();
        }

        [Fact]
        public void Should_Return_True_If_CreatePokemonRequest_Valid()
        {
            // Arrange
            var request = PokemonMock.CreatePokemonRequestFaker;

            // Act
            var validationResult = _validator.Validate(request);
            var isValid = validationResult.IsValid;

            // Assert
            validationResult.Errors.Count.Should().Be(0);
            isValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, null, null, 0d, 0d, null, 0, -1)]
        [InlineData(152, "", "", 0d, 0d, "", 16, 16)]
        public void Should_Return_False_If_CreatePokemonRequest_Invalid(
            int number, string name, string species, double height,
            double weight, string imageUrl, int firstType, int secondType
        )
        {
            // Arrange
            var request = PokemonMock.CreatePokemonRequestFaker
                .RuleFor(x => x.Number, number)
                .RuleFor(x => x.Name, name)
                .RuleFor(x => x.Species, species)
                .RuleFor(x => x.Height, height)
                .RuleFor(x => x.Weight, weight)
                .RuleFor(x => x.ImageUrl, imageUrl)
                .RuleFor(x => x.FirstType, firstType)
                .RuleFor(x => x.SecondType, secondType);

            // Act
            var validationResult = _validator.Validate(request);
            var isValid = validationResult.IsValid;

            // Assert
            validationResult.Errors.Count.Should().Be(8);
            isValid.Should().BeFalse();
        }
    }
}

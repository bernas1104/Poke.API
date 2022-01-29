using FluentAssertions;
using Poke.Core.Tests.Mocks;
using Poke.Core.Validations;
using Xunit;

namespace Poke.Unit.Tests.Core.Validations
{
    public class CreateBaseStatsValidationTest
    {
        private readonly CreateBaseStatsValidation _validator;

        public CreateBaseStatsValidationTest()
        {
            _validator = new CreateBaseStatsValidation();
        }

        [Fact]
        public void Should_Return_True_If_CreateBaseStatsRequest_Valid()
        {
            // Arrange
            var request = BaseStatsMock.CreateBaseStatsFaker;

            // Act
            var validationResult = _validator.Validate(request);
            var isValid = validationResult.IsValid;

            // Assert
            validationResult.Errors.Count.Should().Be(0);
            isValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(256, 256, 256, 256, 256, 256)]
        public void Should_Return_False_If_CreateBaseStatsRequest_Invalid(
            int hitPoints, int attack, int defense, int specialAttack,
            int specialDefense, int speed
        )
        {
            // Arrange
            var request = BaseStatsMock.CreateBaseStatsFaker
                .RuleFor(x => x.HitPoints, hitPoints)
                .RuleFor(x => x.Attack, attack)
                .RuleFor(x => x.Defense, defense)
                .RuleFor(x => x.SpecialAttack, specialAttack)
                .RuleFor(x => x.SpecialDefense, specialDefense)
                .RuleFor(x => x.Speed, speed);

            // Act
            var validationResult = _validator.Validate(request);
            var isValid = validationResult.IsValid;

            // Assert
            validationResult.Errors.Count.Should().Be(6);
            isValid.Should().BeFalse();
        }
    }
}

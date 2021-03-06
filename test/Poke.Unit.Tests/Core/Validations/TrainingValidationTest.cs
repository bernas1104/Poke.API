using FluentAssertions;
using Poke.Core.Tests.Mocks;
using Poke.Core.Validations;
using Xunit;

namespace Poke.Unit.Tests.Core.Validations
{
    public class TrainingValidationTest
    {
        private readonly TrainingValidation _validator;

        public TrainingValidationTest()
        {
            _validator = new TrainingValidation();
        }

        [Fact]
        public void Should_Return_True_If_TrainingDTO_Valid()
        {
            // Arrange
            var request = TrainingMock.TrainingDTOFaker;

            // Act
            var validationResult = _validator.Validate(request);
            var isValid = validationResult.IsValid;

            // Assert
            validationResult.Errors.Count.Should().Be(0);
            isValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, -1, -1)]
        [InlineData(4, 141, 6)]
        public void Should_Return_False_If_TrainingDTO_Invalid(
            int evYeld, int baseFriendship, int growthRate
        )
        {
            // Arrange
            var request = TrainingMock.TrainingDTOFaker
                .RuleFor(x => x.EVYeld, evYeld)
                .RuleFor(x => x.BaseFriendship, baseFriendship)
                .RuleFor(x => x.GrowthRate, growthRate);

            // Act
            var validationResult = _validator.Validate(request);
            var isValid = validationResult.IsValid;

            // Assert
            validationResult.Errors.Count.Should().Be(3);
            isValid.Should().BeFalse();
        }
    }
}

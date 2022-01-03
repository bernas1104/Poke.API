using Bogus;
using FluentAssertions;
using Poke.Core.ValueObjects;
using Xunit;

namespace Poke.Unit.Tests.ValueObjects
{
    public class PointTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(251)]
        public void Should_Instanciate_An_Invalid_Pokemon_Stats_Point(int value)
        {
            // Arrange
            var point = new Point(value);

            // Act
            // Assert
            //
        }

        [Fact]
        public void Should_Instanciate_A_Valid_Pokemon_Stats_Point()
        {
            // Arrange
            var point = new Point(new Faker().Random.Int(1, 250));

            // Act
            // Assert
            //
        }
    }
}

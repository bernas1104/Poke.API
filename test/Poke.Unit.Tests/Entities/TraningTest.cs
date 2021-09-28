using System;
using Bogus;
using FluentAssertions;
using Poke.Core.Entities;
using Poke.Core.Enums;
using Xunit;

namespace Poke.Unit.Tests.Entities
{
    public class TraningTest
    {
        [Theory]
        [InlineData(0, 49, -1)]
        [InlineData(4, 141, 6)]
        public void Should_Create_An_Invalid_Pokemon_Training_Information(
            int evYeld, int baseFrindship, int growthRate
        )
        {
            // Arrange
            var training = new Training(
                evYeld, baseFrindship, (GrowthRate)growthRate
            );

            // Act
            // Assert
            training.IsValid.Should().BeFalse();
            training.Notifications.Count.Should().Be(3);
        }

        [Theory]
        [InlineData(0, 49, -1)]
        [InlineData(4, 141, 6)]
        public void Should_Instanciate_An_Invalid_Pokemon_Training_Information(
            int evYeld, int baseFrindship, int growthRate
        )
        {
            // Arrange
            // Arrange
            var training = new Training(
                Guid.NewGuid(), evYeld, baseFrindship,
                (GrowthRate)growthRate, Guid.NewGuid()
            );

            // Act
            // Assert
            training.IsValid.Should().BeFalse();
            training.Notifications.Count.Should().Be(3);
        }

        [Fact]
        public void Should_Create_A_Valid_Pokemon_Training_Information()
        {
            // Arrange
            var faker = new Faker();

            var training = new Training(
                faker.Random.Int(1, 3), faker.Random.Int(50, 140),
                (GrowthRate)faker.Random.Int(0, 5)
            );

            // Act
            // Assert
            training.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Instanciate_A_Valid_Pokemon_Training_Information()
        {
            // Arrange
            var faker = new Faker();

            var training = new Training(
                Guid.NewGuid(), faker.Random.Int(1, 3), faker.Random.Int(50, 140),
                (GrowthRate)faker.Random.Int(0, 5), Guid.NewGuid()
            );

            // Act
            // Assert
            training.IsValid.Should().BeTrue();
        }
    }
}

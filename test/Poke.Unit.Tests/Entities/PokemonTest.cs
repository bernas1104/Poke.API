using System;
using Bogus;
using FluentAssertions;
using Poke.Core.Entities;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;
using Xunit;

namespace Poke.Unit.Tests.Entities
{
    public class PokemonTest
    {
        [Theory]
        [InlineData(0, "", "", 0d, 0d, "", -1, -1, 10)]
        [InlineData(152, null, null, 0d, 0d, null, 0, 0, 9)]
        [InlineData(0, null, null, 0d, 0d, "", 5, 5, 8)]
        [InlineData(152, "", "", 0d, 0d, null, 16, 16, 10)]
        public void Should_Create_An_Invalid_Pokemon(
            int number, string name, string species,
            double height, double weight, string imageUrl,
            int firstType, int secondType, int expectedNotifications
        )
        {
            // Arrange
            var pokemon = new Pokemon(
                number, name, species, height, weight, imageUrl,
                (PokemonType)firstType, (PokemonType)secondType
            );

            // Act
            // Assert
            pokemon.IsValid.Should().BeFalse();
            pokemon.Notifications.Count.Should().Be(expectedNotifications);
        }

        [Theory]
        [InlineData(0, "", "", 0d, 0d, "", -1, -1, 10)]
        [InlineData(152, null, null, 0d, 0d, null, 0, 0, 9)]
        [InlineData(0, null, null, 0d, 0d, "", 5, 5, 8)]
        [InlineData(152, "", "", 0d, 0d, null, 16, 16, 10)]
        public void Should_Instanciate_An_Invalid_Pokemon(
            int number, string name, string species,
            double height, double weight, string imageUrl,
            int firstType, int secondType, int expectedNotifications
        )
        {
            // Arrange
            var pokemon = new Pokemon(
                number, name, species, height, weight, imageUrl,
                (PokemonType)firstType, (PokemonType)secondType
            );

            // Act
            // Assert
            pokemon.IsValid.Should().BeFalse();
            pokemon.Notifications.Count.Should().Be(expectedNotifications);
        }

        [Fact]
        public void Should_Create_A_Valid_Pokemon()
        {
            // Arrange
            var training = new Training(1, 50, GrowthRate.Fast);

            var point = new Point(50);

            var baseStats = new BaseStats(
                point, point, point, point, point, point
            );

            var faker = new Faker();

            var pokemon = new Pokemon(
                faker.Random.Int(1, 151), faker.Person.FirstName,
                faker.Random.Word(), faker.Random.Double(1, 100),
                faker.Random.Double(1, 100), faker.Internet.Url(),
                (PokemonType)faker.Random.Int(1, 8),
                (PokemonType)faker.Random.Int(9, 15)
            );

            // Act
            // Assert
            pokemon.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Instanciate_A_Valid_Pokemon()
        {
            // Arrange
            var faker = new Faker();

            var pokemon = new Pokemon(
                Guid.NewGuid(), faker.Random.Int(1, 151), faker.Person.FirstName,
                faker.Random.Word(), faker.Random.Double(1, 100),
                faker.Random.Double(1, 100), faker.Internet.Url(),
                (PokemonType)faker.Random.Int(1, 8),
                (PokemonType)faker.Random.Int(9, 15)
            );

            // Act
            // Assert
            pokemon.IsValid.Should().BeTrue();
        }
    }
}

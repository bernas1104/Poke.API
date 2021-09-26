using System;
using FluentAssertions;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;
using Xunit;

namespace Poke.Unit.Tests.Entities
{
    public class BaseStatsTest
    {
        [Theory]
        [InlineData(29, 29, 29, 29, 29, 29)]
        [InlineData(98, 98, 98, 98, 98, 101)]
        public void Should_Create_An_Invalid_Pokemon_Base_Stats(
            int hitPointsValue, int attackValue, int defenseValue,
            int specialAttackValue, int specialDefenseValue, int speedValue
        )
        {
            // Arrange
            var hitPoints = new Point(hitPointsValue);

            var attack = new Point(attackValue);

            var defense = new Point(defenseValue);

            var specialAttack = new Point(specialAttackValue);

            var specialDefense = new Point(specialDefenseValue);

            var speed = new Point(speedValue);

            var baseStats = new BaseStats(
                hitPoints, attack, defense, specialAttack,
                specialDefense, speed
            );

            // Act
            // Assert
            baseStats.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(29, 29, 29, 29, 29, 29)]
        [InlineData(98, 98, 98, 98, 98, 101)]
        public void Should_Instanciate_An_Invalid_Pokemon_Base_Stats(
            int hitPointsValue, int attackValue, int defenseValue,
            int specialAttackValue, int specialDefenseValue, int speedValue
        )
        {
            // Arrange
            var hitPoints = new Point(hitPointsValue);

            var attack = new Point(attackValue);

            var defense = new Point(defenseValue);

            var specialAttack = new Point(specialAttackValue);

            var specialDefense = new Point(specialDefenseValue);

            var speed = new Point(speedValue);

            var baseStats = new BaseStats(
                Guid.NewGuid(), hitPoints, attack, defense,
                specialAttack, specialDefense, speed, Guid.NewGuid()
            );

            // Act
            // Assert
            baseStats.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Create_A_Valid_Pokemon_Base_Stats()
        {
            // Arrange
            var hitPoints = new Point(90);

            var attack = new Point(90);

            var defense = new Point(90);

            var specialAttack = new Point(90);

            var specialDefense = new Point(90);

            var speed = new Point(90);

            var baseStats = new BaseStats(
                hitPoints, attack, defense, specialAttack,
                specialDefense, speed
            );

            // Act
            // Assert
            baseStats.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Instanciate_A_Valid_Pokemon_Base_Stats()
        {
            // Arrange
            var hitPoints = new Point(90);

            var attack = new Point(90);

            var defense = new Point(90);

            var specialAttack = new Point(90);

            var specialDefense = new Point(90);

            var speed = new Point(90);

            var baseStats = new BaseStats(
                Guid.NewGuid(), hitPoints, attack, defense,
                specialAttack, specialDefense, speed, Guid.NewGuid()
            );

            // Act
            // Assert
            baseStats.IsValid.Should().BeTrue();
        }
    }
}

using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Notifications;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class CreatePokemonHandlerTest : HandlerBaseTest
    {
        private IRequestHandler<CreatePokemonRequest, Pokemon> _handler;

        public CreatePokemonHandlerTest()
        {
            _handler = new CreatePokemonHandler(
                _repository.Object,
                _domainNotification.Object,
                _unitOfWork.Object
            );
        }

        [Theory]
        [InlineData(null, 1, 1)]
        [InlineData("Pokemon", 0, 1)]
        [InlineData("Pokemon", 1, 0)]
        public async Task Should_Return_NullPokemon_If_Invalid_Request(
            string pokemonName, int evYeld, int hitPoints
        )
        {
            // Arrrange
            var request = PokemonMock.CreatePokemonRequestFaker
                .RuleFor(x => x.Name, pokemonName)
                .RuleFor(
                    x => x.Training,
                    () => TrainingMock.CreateTrainingRequestFaker
                        .RuleFor(x => x.EVYeld, evYeld)
                )
                .RuleFor(
                    x => x.BaseStats,
                    () => BaseStatsMock.CreateBaseStatsFaker
                        .RuleFor(x => x.HitPoints, hitPoints)
                );

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<NullPokemon>();

            _domainNotification.Verify(
                x => x.AddValidationNotifications(It.IsAny<ValidationResult>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_NullPokemon_If_Pokemon_Number_Exists()
        {
            // Arrange
            var request = PokemonMock.CreatePokemonRequestFaker.Generate();

            _repository.Setup(
                x => x.PokemonExistsAsync(It.IsAny<int>())
            ).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<NullPokemon>();

            _domainNotification.Verify(
                x => x.AddNotification(
                    It.IsAny<NotificationMessage>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_CreatedPokemon_If_Valid_Request()
        {
            // Arrrange
            var request = PokemonMock.CreatePokemonRequestFaker;

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<Pokemon>();

            _domainNotification.Verify(
                x => x.AddValidationNotifications(It.IsAny<ValidationResult>()),
                Times.Never
            );
        }
    }
}

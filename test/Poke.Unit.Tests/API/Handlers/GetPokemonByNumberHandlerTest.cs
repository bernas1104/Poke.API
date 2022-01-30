using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Notifications;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class GetPokemonByNumberHandlerTest : HandlerBaseTest
    {
        private readonly IRequestHandler<GetPokemonByNumberRequest, Pokemon> _handler;

        public GetPokemonByNumberHandlerTest()
        {
            _handler = new GetPokemonByNumberHandler(
                _repository.Object,
                _domainNotification.Object
            );
        }

        [Fact]
        public async Task Should_Return_NullPokemon_If_Pokemon_Not_Found()
        {
            // Arrange
            var request = PokemonMock.GetPokemonByNumberRequestFaker;

            _repository.Setup(x => x.GetByNumberAsync(It.IsAny<int>()))
                .ReturnsAsync(new NullPokemon());

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.IsNull.Should().BeTrue();

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_Pokemon_By_Number()
        {
            // Arrange
            var request = PokemonMock.GetPokemonByNumberRequestFaker;

            _repository.Setup(x => x.GetByNumberAsync(It.IsAny<int>()))
                .ReturnsAsync(PokemonMock.PokemonFaker);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.IsNull.Should().BeFalse();

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Never
            );
        }
    }
}

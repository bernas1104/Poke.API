using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Entities;
using Poke.Core.Notifications;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class GetPokemonEvolutionPairHandlerTest : HandlerBaseTest
    {
        private readonly IRequestHandler<GetPokemonEvolutionPairRequest, List<Pokemon>> _handler;

        public GetPokemonEvolutionPairHandlerTest()
        {
            _handler = new GetPokemonEvolutionPairHandler(
                _repository.Object,
                _domainNotification.Object
            );
        }

        [Fact]
        public async Task Should_Return_Empty_List_If_One_Or_Both_Pokemon_NotFound()
        {
            // Arrange
            var request = PokemonMock.GetPokemonEvolutionPairRequestFaker;

            _repository.Setup(
                x => x.GetPokemonEvolutionPair(It.IsAny<int>(), It.IsAny<int>())
            ).ReturnsAsync(
                PokemonMock.PokemonFaker.Generate(_faker.Random.Int(0, 1))
            );

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Count.Should().Be(0);

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_List_With_Two_Pokemon()
        {
            // Arrange
            var request = PokemonMock.GetPokemonEvolutionPairRequestFaker;

            _repository.Setup(
                x => x.GetPokemonEvolutionPair(It.IsAny<int>(), It.IsAny<int>())
            ).ReturnsAsync(PokemonMock.PokemonFaker.Generate(2));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Count.Should().Be(2);

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Never
            );
        }
    }
}

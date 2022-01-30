using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Entities;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class GetAllPokemonHandlerTest : HandlerBaseTest
    {
        private IRequestHandler<GetAllPokemonRequest, List<Pokemon>> _handler;

        public GetAllPokemonHandlerTest()
        {
            _handler = new GetAllPokemonHandler(
                _repository.Object
            );
        }

        [Fact]
        public async Task Should_Return_List_Of_All_Registered_Pokemon()
        {
            // Arrange
            var request = new GetAllPokemonRequest();

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(
                PokemonMock.PokemonFaker.Generate(_faker.Random.Int(0, 151))
            );

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<List<Pokemon>>();
            result.Count.Should().BeInRange(0, 151);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poke.API.Controllers.V1;
using Poke.Core.Commands.Requests;
using Poke.Core.Commands.Responses;
using Poke.Core.Entities;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Controllers
{
    public class PokemonControllerTest : ControllerBaseTest
    {
        private readonly PokemonsController _controller;

        public PokemonControllerTest()
        {
            _controller = new PokemonsController(
                _mediator.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Deve_Retornar_StatusCode_Created_Ao_Criar_Pokemon()
        {
            // Arrange
            var request = PokemonMock.CreatePokemonRequestFaker;

            _mediator.Setup(
                x => x.Send<Pokemon>(
                    It.IsAny<CreatePokemonRequest>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(PokemonMock.PokemonFaker);

            // Act
            var result = await _controller.CreatePokemonAsync(request);

            // Assert
            result.Should().BeOfType<ActionResult<CreatePokemonResponse>>();
            result.Result.Should().BeOfType<CreatedResult>();
        }
    }
}

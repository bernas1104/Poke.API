using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poke.API.Controllers.V1;
using Poke.Core.Commands.Requests;
using Poke.Core.Commands.Responses;
using Poke.Core.Entities;
using Poke.Core.Queries.Requests;
using Poke.Core.Queries.Response;
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
        public async Task Should_Return_StatusCode_Ok_On_Request_All_Pokemon()
        {
            // Arrange
            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetAllPokemonRequest>(),
                    default
                )
            ).ReturnsAsync(
                PokemonMock.PokemonFaker.
                    Generate(_faker.Random.Int(0, 151))
            );

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().BeOfType<ActionResult<List<PokemonQueryResponse>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_StatusCode_Created_When_Pokemon_Created()
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

        [Fact]
        public async Task Should_Return_StatusCode_Ok_On_Request_Pokemon_By_Number()
        {
            // Arrange
            _mediator.Setup(
                x => x.Send<Pokemon>(
                    It.IsAny<GetPokemonByNumberRequest>(),
                    default
                )
            ).ReturnsAsync(PokemonMock.PokemonFaker);

            // Act
            var result = await _controller.GetByNumberAsync(
                _faker.Random.Int(1, 151)
            );

            // Assert
            result.Should().BeOfType<ActionResult<PokemonQueryResponse>>();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_StatusCode_NoContent_On_Pokemon_Updated_By_Number()
        {
            // Arrange
            var request = PokemonMock.UpdatePokemonByNumberRequestFaker;

            _mediator.Setup(
                x => x.Send<MediatR.Unit>(
                    It.IsAny<UpdatePokemonByNumberRequest>(),
                    default
                )
            ).ReturnsAsync(new MediatR.Unit());

            // Act
            var result = await _controller.UpdateByNumberAsync(
                _faker.Random.Int(1, 151),
                request
            );

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Should_Return_StatusCode_NoContent_On_Pokemon_Deleted_By_Number()
        {
            // Arrange
            _mediator.Setup(
                x => x.Send<MediatR.Unit>(
                    It.IsAny<DeletePokemonByNumberRequest>(),
                    default
                )
            ).ReturnsAsync(new MediatR.Unit());

            // Act
            var result = await _controller.DeleteByNumberAsync(
                _faker.Random.Int(1, 151)
            );

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}

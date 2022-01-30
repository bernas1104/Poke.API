using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class DeletePokemonByNumberHandlerTest : HandlerBaseTest
    {
        private readonly IRequestHandler<DeletePokemonByNumberRequest, MediatR.Unit> _handler;

        public DeletePokemonByNumberHandlerTest()
        {
            _handler = new DeletePokemonByNumberHandler(
                _repository.Object,
                _mediator.Object,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task Should_Delete_Pokemon_By_Number()
        {
            // Arrange
            var request = PokemonMock.DeletePokemonByNumberRequestFaker;

            _mediator.Setup(
                x => x.Send<Pokemon>(
                    It.IsAny<GetPokemonByNumberRequest>(),
                    default
                )
            ).ReturnsAsync(PokemonMock.PokemonFaker);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<MediatR.Unit>();

            _repository.Verify(
                x => x.Remove(It.IsAny<Pokemon>()),
                Times.Once
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Delete_If_Pokemon_Not_Found()
        {
            // Arrange
            var request = PokemonMock.DeletePokemonByNumberRequestFaker;

            _mediator.Setup(
                x => x.Send<Pokemon>(
                    It.IsAny<GetPokemonByNumberRequest>(),
                    default
                )
            ).ReturnsAsync(new NullPokemon());

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<MediatR.Unit>();

            _repository.Verify(
                x => x.Remove(It.IsAny<Pokemon>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }
    }
}

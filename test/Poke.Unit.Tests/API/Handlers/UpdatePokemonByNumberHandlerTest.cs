using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
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
    public class UpdatePokemonByNumberHandlerTest : HandlerBaseTest
    {
        private readonly IRequestHandler<UpdatePokemonByNumberRequest, MediatR.Unit> _handler;

        public UpdatePokemonByNumberHandlerTest()
        {
            _handler = new UpdatePokemonByNumberHandler(
                _repository.Object,
                _mediator.Object,
                _domainNotification.Object,
                _mapper,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task Should_Return_Unit_If_Updated_Data_Invalid()
        {
            // Arrange
            var request = PokemonMock.UpdatePokemonByNumberRequestFaker
                .RuleFor(x => x.Name, () => null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<MediatR.Unit>();

            _domainNotification.Verify(
                x => x.AddValidationNotifications(
                    It.IsAny<ValidationResult>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_Unit_If_Pokemon_To_Update_Not_Found()
        {
            // Arrange
            var request = PokemonMock.UpdatePokemonByNumberRequestFaker;

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
                x => x.Update(It.IsAny<Pokemon>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Should_Return_Unit_If_Pokemon_Updated()
        {
            // Arrange
            var request = PokemonMock.UpdatePokemonByNumberRequestFaker;

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
                x => x.Update(It.IsAny<Pokemon>()),
                Times.Once
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}

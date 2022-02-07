using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Notifications;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class CreatePokemonFamilyHandlerTest : HandlerBaseTest
    {
        private readonly IRequestHandler<CreatePokemonFamilyRequest, List<Pokemon>> _handler;

        public CreatePokemonFamilyHandlerTest()
        {
            _handler = new CreatePokemonFamilyHandler(
                _repository.Object,
                _mapper,
                _domainNotification.Object,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task Should_Return_Empty_Pokemon_List_If_Invalid_Request()
        {
            // Arrange
            var request = PokemonMock.CreatePokemonFamilyRequestFaker
                .RuleFor(
                    x => x.Pokemons,
                    PokemonMock.CreatePokemonWithEvolutionsRequestFaker
                        .RuleFor(x => x.Number, 0)
                        .Generate(1)
                );

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Count.Should().Be(0);

            _domainNotification.Verify(
                x => x.AddValidationNotifications(
                    It.IsAny<ValidationResult>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_Empty_Pokemon_List_If_Any_Pokemon_Already_Exists()
        {
            // Arrange
            var request = PokemonMock.CreatePokemonFamilyRequestFaker;

            _repository.Setup(x => x.PokemonsExistAsync(
                It.IsAny<IEnumerable<int>>())
            ).ReturnsAsync(true);

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
        public async Task Should_Return_List_Created_Pokemon()
        {
            // Arrange
            var request = PokemonMock.CreatePokemonFamilyRequestFaker;

            _repository.Setup(x => x.PokemonsExistAsync(
                It.IsAny<IEnumerable<int>>())
            ).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Count.Should().BeGreaterThanOrEqualTo(1);
            result.Count.Should().BeLessThanOrEqualTo(3);

            _domainNotification.Verify(
                x => x.AddValidationNotifications(
                    It.IsAny<ValidationResult>()
                ),
                Times.Never
            );

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.BeginTransaction(), Times.Once);
            _unitOfWork.Verify(x => x.BeginCommit(), Times.Once);
            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}

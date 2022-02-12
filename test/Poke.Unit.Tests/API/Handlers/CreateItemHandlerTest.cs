using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Notifications;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class CreateItemHandlerTest : HandlerBaseTest
    {
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly IRequestHandler<CreateItemRequest, Item> _handler;

        public CreateItemHandlerTest()
        {
            _itemRepository = new Mock<IItemRepository>();
            _handler = new CreateItemHandler(
                _itemRepository.Object,
                _mapper,
                _domainNotification.Object,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task Should_Return_NullItem_If_Invalid_Request()
        {
            // Arrange
            var request = ItemMock.CreateItemRequestFaker
                .RuleFor(x => x.Name, "");

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<NullItem>();

            _domainNotification.Verify(
                x => x.AddValidationNotifications(It.IsAny<ValidationResult>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_NullItem_If_Item_Already_Exists()
        {
            // Arrange
            var request = ItemMock.CreateItemRequestFaker;

            _itemRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<NullItem>();

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_Created_Item_If_Valid_Request()
        {
            // Arrange
            var request = ItemMock.CreateItemRequestFaker;

            _itemRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<Item>();

            _itemRepository.Verify(x => x.Add(It.IsAny<Item>()), Times.Once);
            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}

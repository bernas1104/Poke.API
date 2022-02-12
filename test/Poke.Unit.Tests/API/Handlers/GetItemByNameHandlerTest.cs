using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Notifications;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class GetItemByNameHandlerTest : HandlerBaseTest
    {
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly IRequestHandler<GetItemByNameRequest, Item> _handler;

        public GetItemByNameHandlerTest()
        {
            _itemRepository = new Mock<IItemRepository>();
            _handler = new GetItemByNameHandler(
                _itemRepository.Object,
                _domainNotification.Object
            );
        }

        [Fact]
        public async Task Should_Return_NullItem()
        {
            // Arrange
            var request = ItemMock.GetItemByNameRequestFaker;

            _itemRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new NullItem());

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
        public async Task Should_Return_Item()
        {
            // Arrange
            var request = ItemMock.GetItemByNameRequestFaker;

            _itemRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(ItemMock.ItemFaker);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<Item>();

            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Never
            );
        }
    }
}

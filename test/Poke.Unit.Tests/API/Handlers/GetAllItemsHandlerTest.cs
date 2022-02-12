using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class GetAllItemsHandlerTest : HandlerBaseTest
    {
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly IRequestHandler<GetAllItemsRequest, List<Item>> _handler;

        public GetAllItemsHandlerTest()
        {
            _itemRepository = new Mock<IItemRepository>();
            _handler = new GetAllItemsHandler(_itemRepository.Object);
        }

        [Fact]
        public async Task Should_Return_List_Of_All_Items()
        {
            // Arrange
            var request = new GetAllItemsRequest();

            _itemRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(
                ItemMock.ItemFaker.Generate(_faker.Random.Int(0, 9))
            );

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            result.Should().BeOfType<List<Item>>();
            result.Count.Should().BeGreaterThanOrEqualTo(0);
            result.Count.Should().BeLessThanOrEqualTo(9);
        }
    }
}

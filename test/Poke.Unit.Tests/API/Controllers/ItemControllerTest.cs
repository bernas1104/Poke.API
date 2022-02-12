using System.Collections.Generic;
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
    public class ItemControllerTest : ControllerBaseTest
    {
        private readonly ItemsController _controller;

        public ItemControllerTest()
        {
            _controller = new ItemsController(
                _mapper,
                _mediator.Object
            );
        }

        [Fact]
        public async Task Should_Return_StatusCode_Created_When_Item_Created()
        {
            // Arrange
            var request = ItemMock.CreateItemRequestFaker;

            _mediator.Setup(
                x => x.Send<Item>(It.IsAny<CreateItemRequest>(), default)
            ).ReturnsAsync(ItemMock.ItemFaker);

            // Act
            var result = await _controller.CreateItemAsync(request);

            // Assert
            result.Should().BeOfType<ActionResult<CreateItemResponse>>();
            result.Result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task Should_Return_StatusCode_Ok_On_Request_All_Items()
        {
            // Arrange
            var request = new GetAllItemsRequest();

            _mediator.Setup(
                x => x.Send<List<Item>>(It.IsAny<GetAllItemsRequest>(), default)
            ).ReturnsAsync(ItemMock.ItemFaker.Generate(_faker.Random.Int(0, 9)));

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            result.Should().BeOfType<ActionResult<List<ItemQueryResponse>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_StatusCode_Ok_On_Request_Item_By_Name()
        {
            // Arrange
            var request = _faker.Random.Word();

            _mediator.Setup(
                x => x.Send<Item>(It.IsAny<GetItemByNameRequest>(), default)
            ).ReturnsAsync(ItemMock.ItemFaker);

            // Act
            var result = await _controller.GetByNameAsync(request);

            // Assert
            result.Should().BeOfType<ActionResult<ItemQueryResponse>>();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}

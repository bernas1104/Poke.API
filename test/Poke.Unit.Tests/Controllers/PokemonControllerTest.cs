// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Bogus;
// using FluentAssertions;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Poke.API.Controllers.V1;
// using Poke.Application.Dtos.InputModels;
// using Poke.Application.Dtos.ViewModels;
// using Poke.Application.Services.Interfaces;
// using Poke.Core.Tests.Mocks;
// using Xunit;

// namespace Poke.Unit.Tests.Controllers
// {
//     public class PokemonControllerTest
//     {
//         private readonly Mock<IPokemonsService> _service;
//         private readonly PokemonsController _controller;

//         public PokemonControllerTest()
//         {
//             _service = new Mock<IPokemonsService>();
//             _controller = new PokemonsController(_service.Object);
//         }

//         [Fact]
//         public async Task Should_Return_A_List_Of_All_Registered_Pokemon()
//         {
//             // Arrange
//             var pokemon = PokemonMock.PokemonViewModelFaker.Generate(
//                 new Faker().Random.Int(0, 151)
//             );

//             _service.Setup(x => x.GetAllAsync()).ReturnsAsync(pokemon);

//             // Act
//             var actionResult = await _controller.GetAllAsync();

//             // Assert
//             actionResult.Result.Should().BeOfType(typeof(OkObjectResult));
//             actionResult.Should().BeOfType(
//                 typeof(ActionResult<IEnumerable<PokemonViewModel>>)
//             );
//         }

//         [Fact]
//         public async Task Should_Return_Created_Status_Code_When_Pokemon_Created()
//         {
//             // Arrange
//             var pokemon = PokemonMock.PokemonInputModelFaker;

//             _service.Setup(
//                 x => x.CreatePokemonAsync(It.IsAny<PokemonInputModel>())
//             ).ReturnsAsync(PokemonMock.PokemonViewModelFaker);

//             // Act
//             var actionResult = await _controller.CreateAsync(pokemon);

//             // Assert
//             actionResult.Result.Should().BeOfType(typeof(CreatedResult));
//             actionResult.Should().BeOfType(typeof(ActionResult<PokemonViewModel>));
//         }
//     }
// }

using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using Poke.Application.Mappings;
using Poke.Application.Services;
using Poke.Application.Services.Interfaces;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.Services
{
    public class PokemonsServiceTest
    {
        private readonly Mock<IPokemonsRepository> _repository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPokemonsService _service;

        public PokemonsServiceTest()
        {
            _repository = new Mock<IPokemonsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.AddProfile(new MappingProfiles())
            ));

            _service = new PokemonsService(
                _repository.Object,
                _unitOfWork.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Should_Not_Create_A_Registered_Pokemon()
        {
            // Arrange
            var pokemonInputModel = PokemonMock.PokemonInputModelFaker
                .Generate();

            var pokemonNumber = pokemonInputModel.Number;

            _repository.Setup(x => x.PokemonExists(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var pokemonViewModel = await _service.CreatePokemonAsync(
                pokemonInputModel
            );

            // Assert
            pokemonViewModel.Should().BeNull();

            _repository.Verify(
                x => x.PokemonExists(It.IsAny<int>()),
                Times.Once
            );

            _repository.Verify(
                x => x.Add(It.IsAny<Pokemon>()),
                Times.Never
            );

            _unitOfWork.Verify(
                x => x.Commit(),
                Times.Never
            );
        }

        [Fact]
        public async Task Should_Not_Create_An_Invalid_Pokemon()
        {
            // Arrange
            var pokemonInputModel = PokemonMock.InvalidPokemonInputModelFaker;

            _repository.Setup(x => x.PokemonExists(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var pokemonViewModel = await _service.CreatePokemonAsync(
                pokemonInputModel
            );

            // Assert
            pokemonViewModel.Should().BeNull();

            _repository.Verify(
                x => x.PokemonExists(It.IsAny<int>()),
                Times.Once
            );

            _repository.Verify(
                x => x.Add(It.IsAny<Pokemon>()),
                Times.Never
            );

            _unitOfWork.Verify(
                x => x.Commit(),
                Times.Never
            );
        }

        [Fact]
        public async Task Should_Create_A_Valid_Pokemon()
        {
            // Arrange
            var pokemonInputModel = PokemonMock.PokemonInputModelFaker;

            _repository.Setup(x => x.PokemonExists(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var pokemonViewModel = await _service.CreatePokemonAsync(
                pokemonInputModel
            );

            // Assert
            pokemonViewModel.Should().NotBeNull();

            _repository.Verify(
                x => x.PokemonExists(It.IsAny<int>()),
                Times.Once
            );

            _repository.Verify(
                x => x.Add(It.IsAny<Pokemon>()),
                Times.Once
            );

            _unitOfWork.Verify(
                x => x.Commit(),
                Times.Once
            );
        }

        [Fact]
        public async Task Should_Return_A_List_Of_All_Registered_Pokemon()
        {
            // Arrange
            var fakeNumber = new Faker().Random.Int(0, 151);

            var pokemon = PokemonMock.PokemonFaker.Generate(fakeNumber);

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(pokemon);

            // Act
            var pokemonViewModel = await _service.GetAllAsync();

            // Assert
            pokemonViewModel.Should().NotBeNull();

            _repository.Verify(
                x => x.GetAllAsync(),
                Times.Once
            );
        }
    }
}

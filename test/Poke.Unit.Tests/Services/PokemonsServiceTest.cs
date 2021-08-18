using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Poke.Application.Dtos.ViewModels;
using Poke.Application.Mappings;
using Poke.Application.Services;
using Poke.Application.Services.Interfaces;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Tests.Mocks;
using Xunit;

namespace Poke.Unit.Tests.Services
{
    public class PokemonsServiceTest
    {
        private readonly Mock<IPokemonsRepository> _pokemonsRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPokemonsService _pokemonsService;

        public PokemonsServiceTest()
        {
            _pokemonsRepository = new Mock<IPokemonsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.AddProfile(new MappingProfiles())
            ));

            _pokemonsService = new PokemonsService(
                _pokemonsRepository.Object,
                _unitOfWork.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Returns_List_Of_All_Registered_Pokemon()
        {
            // Arrange
            _pokemonsRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(
                PokemonMock.PokemonFaker.Generate(151)
            );

            // Act
            var result = await _pokemonsService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<PokemonViewModel>>();
            _pokemonsRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}

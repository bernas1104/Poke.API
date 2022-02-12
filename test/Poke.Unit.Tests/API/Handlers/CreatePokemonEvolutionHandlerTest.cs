using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Enums;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Notifications;
using Poke.Core.Queries.Requests;
using Poke.Core.Tests.Mocks;
using Poke.Core.ValueObjects;
using Xunit;

namespace Poke.Unit.Tests.API.Handlers
{
    public class CreatePokemonEvolutionHandlerTest : HandlerBaseTest
    {
        private readonly Mock<IEvolutionRepository> _evolutionRepository;
        private readonly IRequestHandler<CreatePokemonEvolutionRequest, MediatR.Unit> _handler;

        public CreatePokemonEvolutionHandlerTest()
        {
            _evolutionRepository = new Mock<IEvolutionRepository>();

            _handler = new CreatePokemonEvolutionHandler(
                _evolutionRepository.Object,
                _mediator.Object,
                _mapper,
                _domainNotification.Object,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task Should_Not_Create_Pokemon_Evolution_If_Request_Invalid()
        {
            // Arrange
            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.ToNumber, 1)
                .RuleFor(x => x.FromNumber, 1);

            // Act
            await _handler.Handle(request, default);

            // Assert
            _domainNotification.Verify(
                x => x.AddValidationNotifications(It.IsAny<ValidationResult>()),
                Times.Once
            );

            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Should_Not_Create_Pokemon_Evolution_If_Pokemon_Pair_NotFound()
        {
            // Arrange
            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.FromNumber, f => f.Random.Int(1, 74));

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(
                PokemonMock.PokemonFaker.Generate(_faker.Random.Int(0, 1))
            );

            // Act
            await _handler.Handle(request, default);

            // Assert
            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Should_Not_Create_Pokemon_Evolution_If_Evolution_Already_Registered()
        {
            // Arrange
            var toNumber = _faker.Random.Int(75, 151);
            var fromNumber = _faker.Random.Int(1, 74);

            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.ToNumber, toNumber)
                .RuleFor(x => x.FromNumber, fromNumber);

            var pokemons = new List<Pokemon>
            {
                PokemonMock.PokemonFaker.RuleFor(x => x.Number, fromNumber)
                    .Generate(),
                PokemonMock.PokemonFaker.RuleFor(x => x.Number, toNumber)
                    .Generate()
            };

            SetupPokemonsEvolutions(pokemons);

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(pokemons);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            _domainNotification.Verify(
                x => x.AddNotification(It.IsAny<NotificationMessage>()),
                Times.Once
            );

            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        private void SetupPokemonsEvolutions(List<Pokemon> pokemons)
        {
            pokemons[0].SetPokemonEvolutions(
                new List<AbstractEvolution>
                {
                    EvolutionMock.AbstractEvolutionFaker(true)
                        .RuleFor(x => x.FromNumber, pokemons[0].Number)
                        .RuleFor(x => x.ToNumber, pokemons[1].Number)
                }
            );
            pokemons[0].SetPokemonPreEvolutions(new List<AbstractEvolution>());

            pokemons[1].SetPokemonEvolutions(new List<AbstractEvolution>());
            pokemons[1].SetPokemonPreEvolutions(
                new List<AbstractEvolution>
                {
                    EvolutionMock.AbstractEvolutionFaker(false)
                        .RuleFor(x => x.FromNumber, pokemons[0].Number)
                        .RuleFor(x => x.ToNumber, pokemons[1].Number)
                }
            );
        }

        [Fact]
        public async Task Should_Not_Create_Pokemon_Evolution_If_EvolutionStone_Not_Found()
        {
            // Arrange
            var fromNumber = _faker.Random.Int(1, 74);
            var toNumber = _faker.Random.Int(75, 151);

            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.FromNumber, fromNumber)
                .RuleFor(x => x.ToNumber, toNumber)
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.Stone)
                .RuleFor(x => x.EvolutionStone, f => f.Random.Int(0, 9));

            var pokemons = new List<Pokemon>
            {
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, fromNumber)
                    .Generate(),
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, toNumber)
                    .Generate()
            };

            foreach (var pokemon in pokemons)
            {
                pokemon.SetPokemonEvolutions(new List<AbstractEvolution>());
                pokemon.SetPokemonPreEvolutions(new List<AbstractEvolution>());
            }

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(pokemons);

            _mediator.Setup(
                x => x.Send<Item>(It.IsAny<GetItemByNameRequest>(), default)
            ).ReturnsAsync(new NullItem());

            // Act
            await _handler.Handle(request, default);

            // Assert
            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Should_Not_Create_Pokemon_Evolution_If_HeldItem_Not_Found()
        {
            // Arrange
            var fromNumber = _faker.Random.Int(1, 74);
            var toNumber = _faker.Random.Int(75, 151);

            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.FromNumber, fromNumber)
                .RuleFor(x => x.ToNumber, toNumber)
                .RuleFor(x => x.EvolutionType, (int)EvolutionType.TradeWithItem)
                .RuleFor(x => x.HeldItemName, f => f.Random.Word());

            var pokemons = new List<Pokemon>
            {
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, fromNumber)
                    .Generate(),
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, toNumber)
                    .Generate()
            };

            foreach (var pokemon in pokemons)
            {
                pokemon.SetPokemonEvolutions(new List<AbstractEvolution>());
                pokemon.SetPokemonPreEvolutions(new List<AbstractEvolution>());
            }

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(pokemons);

            _mediator.Setup(
                x => x.Send<Item>(It.IsAny<GetItemByNameRequest>(), default)
            ).ReturnsAsync(new NullItem());

            // Act
            await _handler.Handle(request, default);

            // Assert
            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Never
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Should_Create_Pokemon_Evolution_Without_Item(
            int evolutionType
        )
        {
            // Arrange
            var fromNumber = _faker.Random.Int(1, 74);
            var toNumber = _faker.Random.Int(75, 151);

            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.FromNumber, fromNumber)
                .RuleFor(x => x.ToNumber, toNumber)
                .RuleFor(x => x.EvolutionType, evolutionType);

            var pokemons = new List<Pokemon>
            {
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, fromNumber)
                    .Generate(),
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, toNumber)
                    .Generate()
            };

            foreach (var pokemon in pokemons)
            {
                pokemon.SetPokemonEvolutions(new List<AbstractEvolution>());
                pokemon.SetPokemonPreEvolutions(new List<AbstractEvolution>());
            }

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(pokemons);

            // Act
            await _handler.Handle(request, default);

            // Assert
            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Exactly(2)
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Should_Create_Pokemon_Evolution_With_Item()
        {
            // Arrange
            var fromNumber = _faker.Random.Int(1, 74);
            var toNumber = _faker.Random.Int(75, 151);

            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.FromNumber, fromNumber)
                .RuleFor(x => x.ToNumber, toNumber)
                .RuleFor(x => x.EvolutionStone, f => f.Random.Int(0, 9))
                .RuleFor(x => x.HeldItemName, f => f.Random.Word())
                .RuleFor(
                    x => x.EvolutionType,
                    f => f.Random.Bool() ?
                        (int)EvolutionType.Stone :
                        (int)EvolutionType.TradeWithItem
                );

            var pokemons = new List<Pokemon>
            {
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, fromNumber)
                    .Generate(),
                PokemonMock.PokemonFaker
                    .RuleFor(x => x.Number, toNumber)
                    .Generate()
            };

            foreach (var pokemon in pokemons)
            {
                pokemon.SetPokemonEvolutions(new List<AbstractEvolution>());
                pokemon.SetPokemonPreEvolutions(new List<AbstractEvolution>());
            }

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(pokemons);

            _mediator.Setup(
                x => x.Send<Item>(It.IsAny<GetItemByNameRequest>(), default)
            ).ReturnsAsync(ItemMock.ItemFaker);

            // Act
            await _handler.Handle(request, default);

            // Assert
            _evolutionRepository.Verify(
                x => x.Add(It.IsAny<AbstractEvolution>()),
                Times.Exactly(2)
            );

            _unitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}

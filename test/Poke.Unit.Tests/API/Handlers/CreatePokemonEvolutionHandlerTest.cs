using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Moq;
using Poke.API.Handlers;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
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
        public async Task Should_Create_Pokemon_Evolution()
        {
            // Arrange
            var request = EvolutionMock.CreatePokemonEvolutionRequestFaker
                .RuleFor(x => x.FromNumber, f => f.Random.Int(1, 74));

            _mediator.Setup(
                x => x.Send<List<Pokemon>>(
                    It.IsAny<GetPokemonEvolutionPairRequest>(),
                    default
                )
            ).ReturnsAsync(PokemonMock.PokemonFaker.Generate(2));

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

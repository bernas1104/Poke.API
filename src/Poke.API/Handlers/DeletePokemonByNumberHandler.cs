using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Queries.Requests;

namespace Poke.API.Handlers
{
    public class DeletePokemonByNumberHandler :
        IRequestHandler<DeletePokemonByNumberRequest, Unit>
    {
        private readonly IPokemonRepository _repository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePokemonByNumberHandler(
            IPokemonRepository repository,
            IMediator mediator,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            DeletePokemonByNumberRequest request,
            CancellationToken cancellationToken
        )
        {
            var pokemon = await _mediator.Send<Pokemon>(
                new GetPokemonByNumberRequest { Number = request.Number }
            );

            if (!pokemon.IsNull)
            {
                _repository.Remove(pokemon);
                _unitOfWork.Commit();
            }

            return new Unit();
        }
    }
}

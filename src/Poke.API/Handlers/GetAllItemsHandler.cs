using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Queries.Requests;

namespace Poke.API.Handlers
{
    public class GetAllItemsHandler :
        IRequestHandler<GetAllItemsRequest, List<Item>>
    {
        private readonly IItemRepository _repository;

        public GetAllItemsHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Item>> Handle(
            GetAllItemsRequest request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAllAsync();
        }
    }
}

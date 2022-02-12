using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Notifications;
using Poke.Core.Queries.Requests;

namespace Poke.API.Handlers
{
    public class GetItemByNameHandler :
        IRequestHandler<GetItemByNameRequest, Item>
    {
        private readonly IItemRepository _repository;
        private readonly IDomainNotification _domainNotification;

        public GetItemByNameHandler(
            IItemRepository repository,
            IDomainNotification domainNotification
        )
        {
            _repository = repository;
            _domainNotification = domainNotification;
        }

        public async Task<Item> Handle(
            GetItemByNameRequest request,
            CancellationToken cancellationToken
        )
        {
            var item = await _repository.GetByNameAsync(request.Name);
            if (item.IsNull)
            {
                _domainNotification.AddNotification(
                    new NotificationMessage(
                        "Error",
                        $"Item '{request.Name}' not found",
                        HttpStatusCode.NotFound
                    )
                );
            }

            return item;
        }
    }
}

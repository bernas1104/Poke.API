using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Entities.Nullables;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.Notifications;
using Poke.Core.Validations;

namespace Poke.API.Handlers
{
    public class CreateItemHandler : IRequestHandler<CreateItemRequest, Item>
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDomainNotification _domainNotification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ItemValidation _validator;
        private readonly NullItem Null;

        public CreateItemHandler(
            IItemRepository repository,
            IMapper mapper,
            IDomainNotification domainNotification,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _mapper = mapper;
            _domainNotification = domainNotification;
            _unitOfWork = unitOfWork;

            _validator = new ItemValidation();
            Null = new NullItem();
        }

        public async Task<Item> Handle(
            CreateItemRequest request,
            CancellationToken cancellationToken
        )
        {
            var itemDTO = _mapper.Map<ItemDTO>(request);
            var validationResult = _validator.Validate(itemDTO);
            if (!validationResult.IsValid)
            {
                _domainNotification.AddValidationNotifications(
                    validationResult
                );
                return Null;
            }

            var itemExists = await _repository.ExistsByNameAsync(itemDTO.Name);
            if (itemExists)
            {
                _domainNotification.AddNotification(
                    new NotificationMessage(
                        "Error",
                        $"Item {itemDTO.Name} already exists."
                    )
                );
                return Null;
            }

            var item = Item.FromDTO(itemDTO);
            _repository.Add(item);
            _unitOfWork.Commit();

            return item;
        }
    }
}

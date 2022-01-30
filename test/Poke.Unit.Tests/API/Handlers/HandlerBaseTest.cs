using AutoMapper;
using Bogus;
using MediatR;
using Moq;
using Poke.API.AutoMapper;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;

namespace Poke.Unit.Tests.API.Handlers
{
    public abstract class HandlerBaseTest
    {
        public Faker _faker { get; private set; }
        public Mock<IPokemonRepository> _repository { get; private set; }
        public Mock<IMediator> _mediator { get; private set; }
        public Mock<IDomainNotification> _domainNotification { get; private set; }
        public Mock<IUnitOfWork> _unitOfWork { get; private set; }
        public IMapper _mapper { get; private set; }

        public HandlerBaseTest()
        {
            _faker = new Faker();
            _repository = new Mock<IPokemonRepository>();
            _mediator = new Mock<IMediator>();
            _domainNotification = new Mock<IDomainNotification>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _mapper = new Mapper(
                new MapperConfiguration(
                    cfg => cfg.AddProfile(new MappingProfiles())
                )
            );
        }
    }
}

using Bogus;
using Moq;
using Poke.Core.Interfaces.Notifications;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;

namespace Poke.Unit.Tests.API.Handlers
{
    public abstract class HandlerBaseTest
    {
        public Faker _faker { get; private set; }
        public Mock<IPokemonRepository> _repository { get; private set; }
        public Mock<IDomainNotification> _domainNotification { get; private set; }
        public Mock<IUnitOfWork> _unitOfWork { get; private set; }

        public HandlerBaseTest()
        {
            _faker = new Faker();
            _repository = new Mock<IPokemonRepository>();
            _domainNotification = new Mock<IDomainNotification>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }
    }
}

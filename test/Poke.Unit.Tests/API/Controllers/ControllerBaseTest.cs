using AutoMapper;
using Bogus;
using MediatR;
using Moq;
using Poke.API.AutoMapper;

namespace Poke.Unit.Tests.API.Controllers
{
    public abstract class ControllerBaseTest
    {
        public Faker _faker { get; private set; }
        public Mock<IMediator> _mediator { get; private set; }
        public IMapper _mapper { get; private set; }

        public ControllerBaseTest()
        {
            _faker = new Faker();
            _mediator = new Mock<IMediator>();
            _mapper = new Mapper(
                new MapperConfiguration(
                    cfg => cfg.AddProfile(new MappingProfiles())
                )
            );
        }
    }
}

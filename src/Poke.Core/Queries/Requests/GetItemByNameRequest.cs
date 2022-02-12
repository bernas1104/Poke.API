using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Queries.Requests
{
    public record GetItemByNameRequest : IRequest<Item>
    {
        public string Name { get; init; }
    }
}

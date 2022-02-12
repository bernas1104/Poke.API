using MediatR;
using Poke.Core.Entities;

namespace Poke.Core.Commands.Requests
{
    public class CreateItemRequest : IRequest<Item>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public bool HeldItem { get; init; }
        public int ItemType { get; init; }
    }
}

using System;

namespace Poke.Core.Commands.Responses
{
    public class CreateItemResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool HeldItem { get; init; }
        public int ItemType { get; init; }
        public string ItemTypeDescription { get; init; }
    }
}

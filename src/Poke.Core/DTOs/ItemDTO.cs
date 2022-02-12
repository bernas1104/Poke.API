using System;

namespace Poke.Core.DTOs
{
    public class ItemDTO
    {
        public Guid? Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool HeldItem { get; init; }
        public int ItemType { get; init; }
    }
}

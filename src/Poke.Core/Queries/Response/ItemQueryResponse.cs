using System;

namespace Poke.Core.Queries.Response
{
    public record ItemQueryResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool HeldItem { get; init; }
        public int ItemType { get; init; }
        public string ItemTypeDescription { get; init; }
    }
}

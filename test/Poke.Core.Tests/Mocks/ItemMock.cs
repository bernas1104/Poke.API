using Bogus;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;

namespace Poke.Core.Tests.Mocks
{
    public static class ItemMock
    {
        public static Faker<CreateItemRequest> CreateItemRequestFaker =>
            new Faker<CreateItemRequest>()
                .RuleFor(x => x.Name, f => f.Random.Word())
                .RuleFor(x => x.Description, f => f.Lorem.Sentences())
                .RuleFor(x => x.HeldItem, f => f.Random.Bool())
                .RuleFor(x => x.ItemType, f => f.Random.Int(0, 7));

        public static Faker<Item> ItemFaker => new Faker<Item>();
    }
}

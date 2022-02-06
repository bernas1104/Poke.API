using Poke.Core.Enums;

namespace Poke.Core.Entities
{
    public class Item : Aggregate
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool HeldItem { get; private set; }
        public ItemType ItemType { get; private set; }
    }
}

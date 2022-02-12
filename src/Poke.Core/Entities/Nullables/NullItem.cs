using Poke.Core.Enums;

namespace Poke.Core.Entities.Nullables
{
    public class NullItem : Item
    {
        public NullItem()
        {
            Name = default;
            Description = default;
            HeldItem = default;
            ItemType = (ItemType)(-1);
        }

        public override bool IsNull => true;
    }
}

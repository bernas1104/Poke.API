using Poke.Core.DTOs;
using Poke.Core.Enums;

namespace Poke.Core.Entities
{
    public class Item : Aggregate
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool HeldItem { get; protected set; }
        public ItemType ItemType { get; protected set; }

        public Item()
        {
            //
        }

        protected Item(ItemDTO dto) : base(dto.Id)
        {
            Name = dto.Name;
            Description = dto.Description;
            HeldItem = dto.HeldItem;
            ItemType = (ItemType)dto.ItemType;
        }

        public static Item FromDTO(ItemDTO dto)
        {
            return new Item(dto);
        }
    }
}

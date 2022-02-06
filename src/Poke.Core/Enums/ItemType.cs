using System.ComponentModel;

namespace Poke.Core.Enums
{
    public enum ItemType
    {
        [Description("Item")]
        Item,
        [Description("Poké balls")]
        PokeBalls,
        [Description("Mail")]
        Mail,
        [Description("Battle items")]
        BattleItems,
        [Description("Medicine")]
        Medicine,
        [Description("TM's and HM's")]
        TMsHms,
        [Description("Berries")]
        Berries,
        [Description("Key items")]
        KeyItems
    }
}

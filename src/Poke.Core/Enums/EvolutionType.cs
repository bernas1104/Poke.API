using System.ComponentModel;

namespace Poke.Core.Enums
{
    public enum EvolutionType
    {
        [Description("Level")]
        Level,
        [Description("Stone")]
        Stone,
        [Description("Friendship")]
        Friendship,
        [Description("Trade")]
        Trade,
        [Description("Trade with item")]
        TradeWithItem
    }
}

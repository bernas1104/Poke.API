using System.ComponentModel;

namespace Poke.Core.Enums
{
    public enum GrowthRate
    {
        [Description("Erratic")]
        Erratic,
        [Description("Fast")]
        Fast,
        [Description("Medium Fast")]
        MediumFast,
        [Description("Medium Slow")]
        MediumSlow,
        [Description("Slow")]
        Slow,
        [Description("Fluctuating")]
        Fluctuating
    }
}

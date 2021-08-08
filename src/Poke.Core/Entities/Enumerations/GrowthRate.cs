using System.ComponentModel;

namespace Poke.Core.Entities.Enumerations
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
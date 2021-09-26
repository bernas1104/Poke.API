using Poke.Core.Enums;

namespace Poke.Application.Dtos.ViewModels
{
    public record BaseStatsViewModel
    {
        public int HitPoints { get; init; }
        public int Attack { get; init; }
        public int Defense { get; init; }
        public int SpecialAttack { get; init; }
        public int SpecialDefense { get; init; }
        public int Speed { get; init; }
        public int TotalPoints {
            get => HitPoints + Attack + Defense + SpecialAttack +
                SpecialDefense + Speed;
        }
    }
}

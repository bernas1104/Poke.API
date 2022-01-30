using System;

namespace Poke.Core.DTOs
{
    public record BaseStatsDTO
    {
        public Guid Id { get; init; }
        public int HitPoints { get; init; }
        public int Attack { get; init; }
        public int Defense { get; init; }
        public int SpecialAttack { get; init; }
        public int SpecialDefense { get; init; }
        public int Speed { get; init; }
    }
}

using System;

namespace Poke.Infra.DTO
{
    public record BaseStatsDTO
    {
        public Guid Id { get; init; }
        public int HitPoints { get; init; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
    }
}
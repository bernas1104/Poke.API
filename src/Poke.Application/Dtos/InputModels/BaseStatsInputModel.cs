namespace Poke.Application.Dtos.InputModels
{
    public record BaseStatsInputModel
    {
        public int HitPoints { get; init; }
        public int Attack { get; init; }
        public int Defense { get; init; }
        public int SpecialAttack { get; init; }
        public int SpecialDefense { get; init; }
        public int Speed { get; init; }
    }
}

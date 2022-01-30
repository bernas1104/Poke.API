namespace Poke.Core.Queries.Response
{
    public record BaseStatsQueryResponse
    {
        public int HitPoints { get; init; }
        public int Attack { get; init; }
        public int Defense { get; init; }
        public int SpecialAttack { get; init; }
        public int SpecialDefense { get; init; }
        public int Speed { get; init; }
        public int TotalPoints { get; init; }
    }
}

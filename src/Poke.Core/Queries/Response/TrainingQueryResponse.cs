namespace Poke.Core.Queries.Response
{
    public record TrainingQueryResponse
    {
        public int EVYeld { get; init; }
        public int BaseFriendship { get; init; }
        public int GrowthRate { get; init; }
        public string GrowthRateDescription { get; init; }
    }
}

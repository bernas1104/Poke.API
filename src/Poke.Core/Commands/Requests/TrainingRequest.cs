namespace Poke.Core.Commands.Requests
{
    public record TrainingRequest
    {
        public int EVYeld { get; init; }
        public int BaseFriendship { get; init; }
        public int GrowthRate { get; init; }
    }
}

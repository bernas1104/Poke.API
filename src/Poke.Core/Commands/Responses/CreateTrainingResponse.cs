namespace Poke.Core.Commands.Responses
{
    public record CreateTrainingResponse
    {
        public int EVYeld { get; init; }
        public int BaseFriendship { get; init; }
        public string GrowthRate { get; init; }
    }
}

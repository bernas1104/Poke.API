namespace Poke.Application.Dtos.ViewModels
{
    public record TrainingViewModel
    {
        public int EVYeld { get; init; }
        public int BaseFriendship { get; init; }
        public string GrowthRate { get; init; }
    }
}

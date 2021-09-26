using Poke.Core.Enums;

namespace Poke.Application.Dtos.InputModels
{
    public record TrainingInputModel
    {
        public int EVYeld { get; init; }
        public int BaseFriendship { get; init; }
        public GrowthRate GrowthRate { get; init; }
    }
}

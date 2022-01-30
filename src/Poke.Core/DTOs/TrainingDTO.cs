using System;

namespace Poke.Core.DTOs
{
    public record TrainingDTO
    {
        public Guid Id { get; init; }
        public int EVYeld { get; init; }
        public int BaseFriendship { get; init; }
        public int GrowthRate { get; init; }
    }
}

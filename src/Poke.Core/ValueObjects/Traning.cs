using Poke.Core.Entities.Enumerations;

namespace Poke.Core.ValueObjects
{
    public class Traning
    {
        public int Id { get; set; }
        public int EVYeld { get; set; }
        public double CatchRate { get; set; }
        public int BaseFriendship { get; set; }
        public int BaseExperience { get; set; }
        public GrowthRate GrowthRate { get; set; }
    }
}
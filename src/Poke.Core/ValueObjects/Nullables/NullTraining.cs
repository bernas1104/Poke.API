using Poke.Core.Entities.Nullables;

namespace Poke.Core.ValueObjects.Nullables
{
    public class NullTraining : Training
    {
        public NullTraining()
        {
            EVYeld = default;
            BaseFriendship = default;
            GrowthRate = default;
        }

        public override bool IsNull => true;
    }
}

using Poke.Core.Models.Nullables;

namespace Poke.Core.ValueObjects.Nullables
{
    public class NullBaseStats : BaseStats
    {
        public NullBaseStats()
        {
            HitPoints = new NullPoint();
            Attack = new NullPoint();
            Defense = new NullPoint();
            SpecialAttack = new NullPoint();
            SpecialDefense = new NullPoint();
            Speed = new NullPoint();
        }

        public override bool IsNull => true;
    }
}

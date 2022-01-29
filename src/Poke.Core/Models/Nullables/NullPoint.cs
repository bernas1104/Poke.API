namespace Poke.Core.Models.Nullables
{
    public class NullPoint : Point
    {
        public NullPoint() : base(default)
        {
            //
        }

        public override bool IsNull => true;
    }
}

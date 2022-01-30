namespace Poke.Core.Models
{
    public class Point
    {
        public int Value { get; protected set; }

        public Point(int value)
        {
            Value = value;
        }

        public virtual bool IsNull => false;
    }
}

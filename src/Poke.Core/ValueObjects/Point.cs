namespace Poke.Core.ValueObjects
{
    public class Point : ValueObject
    {
        public int Value { get; private set; }

        public Point(int value)
        {
            Value = value;
        }

        public override bool IsValid()
        {
            return false;
        }
    }
}

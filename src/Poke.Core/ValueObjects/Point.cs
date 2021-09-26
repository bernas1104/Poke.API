using Flunt.Validations;
using Poke.Shared.ValueObjects;

namespace Poke.Core.ValueObjects
{
    public class Point : ValueObject
    {
        public int Value { get; private set; }

        public Point(int value)
        {
            Value = value;

            Validate();
        }

        protected override void Validate()
        {
            AddNotifications(
                new Contract<Point>()
                    .Requires()
                    .IsGreaterOrEqualsThan(
                        Value, 1,
                        "Points should be greater than or equal to one (1)."
                    )
                    .IsLowerOrEqualsThan(
                        Value, 250,
                        "Points should be lower than or equal to two hundred fifty (250)."
                    )
            );
        }
    }
}

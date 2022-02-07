using System;
using System.Collections.Generic;
using System.Linq;

namespace Poke.Core.ValueObjects
{
    public abstract class ValueObject
    {
        public Guid Id { get; private set; }
        public virtual bool IsNull => false;

        public ValueObject()
        {
            //
        }

        public ValueObject(Guid id)
        {
            Id = id;
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = obj as AbstractEvolution;

            return this.GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x is not null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}

using System;

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
    }
}

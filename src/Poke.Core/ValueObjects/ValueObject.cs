using System;

namespace Poke.Core.ValueObjects
{
    public abstract class ValueObject
    {
        public Guid Id { get; protected set; }
        public virtual bool IsNull => false;
    }
}

using System;

namespace Poke.Core.Entities
{
    public abstract class Aggregate
    {
        public Guid Id { get; protected set; }

        public Aggregate()
        {
            //
        }

        public Aggregate(Guid id)
        {
            Id = id;
        }

        public abstract bool IsValid();
    }
}

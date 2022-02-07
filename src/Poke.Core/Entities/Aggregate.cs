using System;

namespace Poke.Core.Entities
{
    public abstract class Aggregate
    {
        public Guid Id { get; private set; }

        public Aggregate()
        {
            //
        }

        public Aggregate(Guid? id)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
        }

        public virtual bool IsNull => false;
    }
}

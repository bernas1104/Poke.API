using System;
using Flunt.Notifications;

namespace Poke.Shared.Entities
{
    public abstract class Aggregate : Notifiable<Notification>
    {
        public Guid Id { get; private set; }

        public Aggregate()
        {
            Id = Guid.NewGuid();
        }

        public Aggregate(Guid id)
        {
            Id = id;
        }

        protected abstract void Validate();
    }
}

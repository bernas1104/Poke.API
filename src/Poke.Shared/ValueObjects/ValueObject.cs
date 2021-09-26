using Flunt.Notifications;

namespace Poke.Shared.ValueObjects
{
    public abstract class ValueObject : Notifiable<Notification>
    {
        protected abstract void Validate();
    }
}

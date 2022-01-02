using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using Poke.Core.Notifications;

namespace Poke.Core.Interfaces.Notifications
{
    public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(NotificationMessage message);
        void AddValidationNotifications(ValidationResult validationResult);
    }
}

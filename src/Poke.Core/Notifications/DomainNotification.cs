using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Poke.Core.Interfaces.Notifications;

namespace Poke.Core.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        public IReadOnlyCollection<NotificationMessage> Notifications { get => _notifications; }
        public bool HasNotifications { get => _notifications.Any(); }
        private List<NotificationMessage> _notifications;

        public DomainNotification()
        {
            _notifications = new List<NotificationMessage>();
        }

        public void AddNotification(NotificationMessage message)
        {
            _notifications.Add(message);
        }

        public void AddValidationNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Add(
                    new NotificationMessage(
                        error.ErrorCode, error.ErrorMessage
                    )
                );
            }
        }
    }
}

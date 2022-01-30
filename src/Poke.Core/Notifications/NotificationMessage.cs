using System;
using System.Net;

namespace Poke.Core.Notifications
{
    public class NotificationMessage
    {
        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }

        public NotificationMessage(
            string key, string value,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
        )
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
            HttpStatusCode = httpStatusCode;
        }
    }
}

using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Poke.Core.Interfaces.Notifications;

namespace Poke.API.Filters
{
    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;

        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next
        )
        {
            if(
                !context.ModelState.IsValid ||
                _domainNotification.HasNotifications
            )
            {
                var validations = !context.ModelState.IsValid ?
                    JsonConvert.SerializeObject(
                        context.ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                    ) :
                    JsonConvert.SerializeObject(
                        _domainNotification.Notifications
                            .Select(x => x.Value)
                    );

                var domainNotification = _domainNotification.Notifications
                    .FirstOrDefault();

                var problemDetails = new ProblemDetails
                {
                    Title = domainNotification is not null ?
                        domainNotification.HttpStatusCode
                            .AsString(EnumFormat.Name) :
                        "Bad Request",
                    Status = domainNotification is not null ?
                        (int)domainNotification.HttpStatusCode :
                        (int)HttpStatusCode.BadRequest,
                    Instance = context.HttpContext.Request.Path.Value,
                    Detail = validations
                };

                context.HttpContext.Response.StatusCode = problemDetails.Status
                    .Value;
                context.HttpContext.Response.ContentType =
                    "application/problem+json";

                using var writer = new StreamWriter(
                    context.HttpContext.Response.Body
                );

                new JsonSerializer().Serialize(writer, problemDetails);

                await writer.FlushAsync();

                await next();
            }

            await next();
        }
    }
}

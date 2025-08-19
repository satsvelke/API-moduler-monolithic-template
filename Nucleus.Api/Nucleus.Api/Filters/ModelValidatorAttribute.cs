using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nucleus.Models;

namespace Nucleus.Api.Filters;


[AttributeUsage(AttributeTargets.Class)]
public sealed class ModelValidatorAttributeAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        ArgumentNullException.ThrowIfNull(context);

        if (!context.ModelState.IsValid)
        {
            var messages = context.ModelState.Select(c => new MessageElement()
            {
                Code = "NucX200",
                ietf = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Message = c.Value?.Errors.Count > 0 ? c.Value.Errors?.FirstOrDefault()!.ErrorMessage.ToString() : string.Empty,
                Type = "Error",
            });


            var traceId = Activity.Current?.Id ?? context?.HttpContext.TraceIdentifier;

            var apiResponse = new ApiResponse()
            {
                MessageHeader = new MessageHeader()
                {
                    Messages = messages.ToList()
                },
                TraceId = traceId
            };

            if (messages?.Count() > 0)
                context!.Result = new BadRequestObjectResult(apiResponse);
        }
        else
        {
            ArgumentNullException.ThrowIfNull(next);

            await next().ConfigureAwait(true);
        }

    }
}

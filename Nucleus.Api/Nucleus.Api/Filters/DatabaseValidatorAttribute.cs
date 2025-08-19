using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nucleus.Models;
using Nucleus.IWorkflow;
using Nucleus.Workflow;

namespace Nucleus.Api.Filters;


[AttributeUsage(AttributeTargets.Method)]
public sealed class DatabaseValidatorAttribute : TypeFilterAttribute
{
    public DatabaseValidatorAttribute(string StoredProcedure) : base(typeof(DbValidator))
    {
        this.Arguments = new object[] { StoredProcedure };
    }

    public string? StoredProcedure { get; }
}

public sealed class DbValidator(IValidationWorkflow validationWorkflow, string storedProcedure) : IAsyncActionFilter
{

    private readonly IValidationWorkflow validationWorkflow = validationWorkflow;
    private readonly string storedProcedure = storedProcedure;
    async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        ArgumentNullException.ThrowIfNull(context);

        var validationParameters = new DatbaseValidation()
        {
            RequestPayload = System.Text.Json.JsonSerializer.Serialize(context.ActionArguments.Values.FirstOrDefault()),
            StoredProcedure = storedProcedure
        };

        var messagesElemets = await validationWorkflow.Validate(validationParameters, new CancellationToken()).ConfigureAwait(true);


        if (messagesElemets is not null && messagesElemets.Count > 0)
        {
            var apiResponse = new ApiResponse()
            {
                MessageHeader = new MessageHeader()
                {
                    Messages = messagesElemets
                }
            };

            context.Result = new BadRequestObjectResult(apiResponse);
        }
        else
        {
            await next().ConfigureAwait(true);
        }

    }
}
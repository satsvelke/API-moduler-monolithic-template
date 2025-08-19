using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nucleus.Models;

namespace Nucleus.Api;

public static class ApiResponseHandler
{
    public static IActionResult ToOk<TResult>(this TResult result, IOptions<MessageHeader> options, HttpContext httpContext, string code)
    {
        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        return new OkObjectResult(new ApiResponse()
        {
            MessageHeader = new MessageHeader()
            {
                Messages = options?.Value?.Messages?.Where(c => c.Code == code).ToList()
            },
            TraceId = traceId,
            Transaction = result
        });
    }

    public static IActionResult ToBadRequest<TResult>(this TResult result, IOptions<MessageHeader> options, HttpContext httpContext, string code)
    {
        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        return new BadRequestObjectResult(new ApiResponse()
        {
            MessageHeader = new MessageHeader()
            {
                Messages = options?.Value?.Messages?.Where(c => c.Code == code).ToList()
            },
            TraceId = traceId,
            Transaction = result
        });
    }

    public static IActionResult ToNotFoundRequest<TResult>(this TResult result, IOptions<MessageHeader> options, HttpContext httpContext, string code)
    {
        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        return new NotFoundObjectResult(new ApiResponse()
        {
            MessageHeader = new MessageHeader()
            {
                Messages = options?.Value?.Messages?.Where(c => c.Code == code).ToList()
            },
            TraceId = traceId,
            Transaction = result
        });
    }

    public static ApiResponse ToApiResponse(this IOptions<MessageHeader> options, string errorCode, HttpContext httpContext)
    {
        return new ApiResponse()
        {
            MessageHeader = new MessageHeader()
            {
                Messages = options?.Value?.Messages?.Where(c => c.Code == errorCode).ToList()
            },
            TraceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier
        };
    }
}

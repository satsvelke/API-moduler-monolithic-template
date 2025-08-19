using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Nucleus.Api.JwtConfig;
using Nucleus.Models;

namespace Nucleus.Api.Filters;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AuthenticationAttribute : Attribute, IAsyncAuthorizationFilter
{

    private readonly IOptions<JwtSettings> jwtSettings;
    private readonly IOptions<MessageHeader> messageHeader;

    private readonly IJwtToken jwtToken;
    public AuthenticationAttribute(IOptions<JwtSettings> jwtSettings, IOptions<MessageHeader> messageHeader, IJwtToken jwtToken)
    {
        this.jwtSettings = jwtSettings;
        this.messageHeader = messageHeader;
        this.jwtToken = jwtToken;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!IsAllowedAnonymous(context))
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrWhiteSpace(token))
            {
                GetUnAuthorizedResponse(context);

                return;
            }

            var isTokenValidated = await jwtToken.ValidateToken(new JwtSettings()
            {
                Audience = jwtSettings.Value.Audience,
                Expiry = jwtSettings.Value.Expiry,
                Issuer = jwtSettings.Value.Issuer,
                Secret = jwtSettings.Value.Secret
            }, token.ToString()).ConfigureAwait(true);

            if (!isTokenValidated)
            {
                GetUnAuthorizedResponse(context);

                return;
            }
        }

    }

    private void GetUnAuthorizedResponse(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(messageHeader.ToApiResponse("AuthX100", context.HttpContext))
        {
            StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized, CultureInfo.InvariantCulture),
        };
    }

    private static bool IsAllowedAnonymous(AuthorizationFilterContext context)
    {
        return context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
    }

    public IOptions<JwtSettings>? JwtSettings { get; }
    public IOptions<MessageHeader>? MessageHeader { get; }
    public IJwtToken? JwtToken { get; }
}

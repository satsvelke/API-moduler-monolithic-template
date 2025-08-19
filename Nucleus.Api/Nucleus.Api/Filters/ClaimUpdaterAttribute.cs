using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Nucleus.Api.JwtConfig;
using Nucleus.Dtos;
using Nucleus.Models;


namespace Nucleus.Api.Filters;


[AttributeUsage(AttributeTargets.Class)]
public sealed class ClaimUpdaterAttribute : Attribute, IAsyncActionFilter
{

    private readonly IOptions<JwtSettings> jwtSettings;
    private readonly IJwtToken jwtToken;

    public ClaimUpdaterAttribute(IOptions<JwtSettings> jwtSettings, IJwtToken jwtToken)
    {
        this.jwtSettings = jwtSettings;
        this.jwtToken = jwtToken;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        ArgumentNullException.ThrowIfNull(context);

        var token = context.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrWhiteSpace(token))
        {
            foreach (var key in context.ActionArguments!.Keys)
            {
                var currentObject = context.ActionArguments[key];

                if (currentObject is not null && currentObject!.GetType().GetProperty("ActiveStaffId") is not null)
                {
                    var userClaims = await jwtToken.GetClaims(new JwtSettings()
                    {
                        Audience = jwtSettings.Value.Audience,
                        Expiry = jwtSettings.Value.Expiry,
                        Secret = jwtSettings.Value.Secret,
                        Issuer = jwtSettings.Value.Issuer
                    }, token!).ConfigureAwait(true);

                    if (userClaims is not null)
                    {
                        var activeUser = new ActiveUserDto();

                        var activeFirstNameProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveFirstName));
                        var activeLastNameProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveLastName));
                        var activeEmailProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveEmail));
                        var activeRoleProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveRoleName));
                        // var activeUserIdProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveUserId));
                        var activeStaffIdProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveStaffId));
                        var activeRoleIdProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveRoleId));
                        var activeCompanyIdProperty = currentObject.GetType().GetProperty(nameof(activeUser.ActiveCompanyId));




                        activeFirstNameProperty!.SetValue(currentObject, userClaims.FirstName);
                        activeLastNameProperty!.SetValue(currentObject, userClaims.LastName);
                        activeEmailProperty!.SetValue(currentObject, userClaims.Email);
                        activeRoleProperty!.SetValue(currentObject, userClaims.RoleName);
                        // activeUserIdProperty!.SetValue(currentObject, userClaims.UserId);
                        activeStaffIdProperty!.SetValue(currentObject, userClaims.StaffId);
                        activeRoleIdProperty!.SetValue(currentObject, userClaims.RoleId);
                        activeCompanyIdProperty!.SetValue(currentObject, userClaims.CompanyId);
                    }
                }
            }
        }

        await next().ConfigureAwait(true);

    }

    public IOptions<JwtSettings>? JwtSettings { get; }
    public IJwtToken? JwtToken { get; }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog.Ui.Web.Authorization;

namespace Nucleus.Api.Filters;

public class SerilogAuthenticationFilter : IUiAuthorizationFilter
{
    public bool Authorize(HttpContext httpContext)
    {
        var authKey = httpContext?.Request.Headers["Authorization"];

        var appSettingsText = File.ReadAllText("appsettings.json");

        if (string.IsNullOrWhiteSpace(appSettingsText)) return false;
        var appSettings = JsonConvert.DeserializeObject<dynamic>(appSettingsText);

        if (appSettings is null) return false;

        return appSettings.ErrorLogKey == authKey;
    }
}

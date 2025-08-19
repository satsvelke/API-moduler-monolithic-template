using Microsoft.AspNetCore.Builder;
using Nucleus.Api.Filters;
using Serilog.Ui.Web;

namespace Nucleus.Api.Dependency;

public static class SerilogUiDependency
{
    public static void UseSerilogUiDependency(this WebApplication app)
    {
        app.UseSerilogUi(options =>
        {
            options.Authorization.AuthenticationType = AuthenticationType.Jwt;
            options.Authorization.Filters = new[]
            {
                new SerilogAuthenticationFilter()
            };

            options.RoutePrefix = "error-logs";
        });
    }
}

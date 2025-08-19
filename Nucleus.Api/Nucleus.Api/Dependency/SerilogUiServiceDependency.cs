using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Ui.MySqlProvider;
using Serilog.Ui.Web;

namespace Nucleus.Api.Dependency;

public static class SerilogUiServiceDependency
{
    public static void RegisterSerilogUiDependency(this WebApplicationBuilder builder)
    {

        ArgumentNullException.ThrowIfNull(builder);

        var errorLogConnectionString = builder.Configuration.GetConnectionString("ErrorLogConnectionString");

        builder.Services.AddSerilogUi(options =>
               options.UseMySqlServer(errorLogConnectionString, "ErrorLogs"));
    }
}

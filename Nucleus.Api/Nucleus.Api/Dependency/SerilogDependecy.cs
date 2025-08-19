using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;

namespace Nucleus.Api.Dependency;

public static class SerilogDependecy
{
    public static void RegisterSerilog(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var serilogPath = builder.Configuration.GetSection("SerilogPath");

        ArgumentException.ThrowIfNullOrWhiteSpace(serilogPath?.Value);

        var errorLogConnectionString = builder.Configuration.GetConnectionString("ErrorLogConnectionString");

        ArgumentException.ThrowIfNullOrWhiteSpace(errorLogConnectionString);


        Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Debug()
                     .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                      .WriteTo.Logger(x => x.Filter.ByIncludingOnly(k => k.Level == LogEventLevel.Error || k.Level == LogEventLevel.Fatal)
                      .WriteTo.MariaDB(
                        connectionString: errorLogConnectionString,
                        tableName: "ErrorLogs",
                        autoCreateTable: true,
                        useBulkInsert: false,
                        options: new MariaDBSinkOptions()
                        {

                        },
                         formatProvider: CultureInfo.InvariantCulture
                        ))
               .WriteTo.Logger(c => c.Filter.ByIncludingOnly(e => e.Level != LogEventLevel.Error)
               .WriteTo.File(
                new JsonFormatter()
               , serilogPath.Value
               , rollingInterval: RollingInterval.Day
               , rollOnFileSizeLimit: true))
               .CreateLogger();
    }
}

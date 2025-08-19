using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Models;

namespace Nucleus.Api.Dependency;

public static class ConfigureAppSettings
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // configure error codes 
        builder.Services.Configure<MessageHeader>(builder.Configuration.GetSection("MessageHeader"));

        //configure jwt settings 
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


    }
}

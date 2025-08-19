using Microsoft.AspNetCore.Builder;

namespace Nucleus.Api.Dependency;

public static class NucleusDependency
{
    public static void RegisterNucleasDependency(this WebApplicationBuilder builder)
    {

        ArgumentNullException.ThrowIfNull(builder);

        // configure all app settings mapping 
        builder.Configure();

        builder.Services.RegisterServiceDependency();

        builder.Services.RegisterJwtDependency(builder.Configuration);

        builder.RegisterSerilog();
    }
}

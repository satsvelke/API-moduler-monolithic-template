using Microsoft.Extensions.DependencyInjection;
using Nucleus.Api.Filters;
using Nucleus.Api.JwtConfig;
using Nucleus.Databases;
using Nucleus.Databases.Interfaces;
using Nucleus.IRepository;
using Nucleus.IWorkflow;
using Nucleus.Repository;
using Nucleus.Workflow;

namespace Nucleus.Api.Dependency;

public static class ServiceDependency
{
    public static void RegisterServiceDependency(this IServiceCollection services)
    {

        services.AddScoped<ClaimUpdaterAttribute>();
        services.AddScoped<AuthenticationAttribute>();
        services.AddScoped<ModelValidatorAttributeAttribute>();



        /// main database context
        services.AddScoped<IMainDatabaseContext, MainDatabaseContext>();

        // jwt token
        services.AddScoped<IJwtToken, JwtToken>();

        // validations dependency 
        services.AddScoped<IValidationRepository, ValidationRepository>();
        services.AddScoped<IValidationWorkflow, ValidationWorkflow>();

    }
}

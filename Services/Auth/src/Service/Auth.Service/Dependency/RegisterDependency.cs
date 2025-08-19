using Auth.IRepository;
using Auth.IWorkflow;
using Auth.Repository;
using Auth.Workflow;

namespace Auth.Service;

public static class RegisterDependency
{
    public static void Register(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUserWorkflow, UserWorkflow>();

    }
}

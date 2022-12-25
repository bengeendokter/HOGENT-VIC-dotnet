using Microsoft.Extensions.DependencyInjection;
using Services.Activities;
using Services.Clients;
using Services.VirtualMachines;
using Shared.Activities;
using Shared.Clients;
using Shared.VirtualMachines;

namespace Services;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IVirtualMachineService, VirtualMachineService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IVirtualMachineRequestService, VirtualMachineRequestService>();
        services.AddScoped<AuthUserService, AuthUserService>();

        // Add more services here...

        return services;
    }
}

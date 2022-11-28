using Shared.VirtualMachines;
using Services.VirtualMachines;
using Shared.Clients;
using Services.Clients;
using Shared.Activities;
using Services.Activities;

using Microsoft.Extensions.DependencyInjection;

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

        // Add more services here...

        return services;
    }
}

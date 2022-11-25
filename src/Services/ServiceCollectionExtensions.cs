using Shared.VirtualMachines;
using Services.VirtualMachines;
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

        // Add more services here...

        return services;
    }
}

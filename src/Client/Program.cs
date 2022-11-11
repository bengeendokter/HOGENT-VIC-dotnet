global using Shared;
global using Shared.VirtualMachines;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.VirtualMachines;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IVirtualMachineService, VirtualMachineService>();
builder.Services.AddSingleton<IVirtualMachineRequestService, VirtualMachineRequestService>();

builder.Services.AddSingleton<IUsageService, FakeUsageStatsService>();
builder.Services.AddSingleton<IActivityService, ActivityService>();

await builder.Build().RunAsync();

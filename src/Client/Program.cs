global using Shared;
global using Shared.VirtualMachines;
global using Shared.Clients; 
global using Client.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.VirtualMachines;
using Client.Users;
using Client.Analytics;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);

builder.Services.AddScoped<IVirtualMachineService, VirtualMachineService>();
builder.Services.AddScoped<IVirtualMachineRequestService, VirtualMachineRequestService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUsageService, FakeUsageStatsService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

await builder.Build().RunAsync();


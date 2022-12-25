global using Client.Extensions;
global using Shared;
global using Shared.Activities;
global using Shared.Clients;
global using Shared.VirtualMachines;
using Client;
using Client.Analytics;
using Client.AuthShared;
using Client.Clients;
using Client.Users;
using Client.VirtualMachines;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);

builder.Services.AddScoped<IVirtualMachineService, VirtualMachineService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IVirtualMachineRequestService, VirtualMachineRequestService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUsageService, FakeUsageStatsService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

builder.Services.AddHttpClient("VICHogentAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
       .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
       .CreateClient("VICHogentAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();


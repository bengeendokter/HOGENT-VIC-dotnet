using Persistence;
using Server.Middleware;
using Services;
using Shared.VirtualMachines;
using Shared.Clients;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Server.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices();

// Fluentvalidation
builder.Services.AddValidatorsFromAssemblyContaining<VirtualMachineDto.Mutate.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClientDto.Validator>();
builder.Services.AddFluentValidationAutoValidation();

// Swagger | OAS
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen(options =>
    {
        // Since we subclass our dto's we need a more unique id.
        options.CustomSchemaIds(
            type =>
                type.DeclaringType is null
                    ? $"{type.Name}"
                    : $"{type.DeclaringType?.Name}.{type.Name}"
        );
        options.EnableAnnotations();
    })
    .AddFluentValidationRulesToSwagger();

// Database
builder.Services.AddDbContext<VicDbContext>(options =>
{
    var (sshClient, localPort) = SshUtil.ConnectSsh(builder.Configuration["MySql:Server"], builder.Configuration["MySql:SshUser"], sshKeyFile: builder.Configuration["MySql:SshKey"]);
    var connectionString = new MySqlConnectionStringBuilder
    {
        Server = "127.0.0.1",
        Port = localPort,
        UserID = builder.Configuration["MySql:User"],
        Password = builder.Configuration["MySql:Password"],
        Database = builder.Configuration["MySql:Database"]
    }.ConnectionString;

    options
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Warning)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

// (Fake) Authentication
/*builder.Services
    .AddAuthentication("Fake Authentication")
    .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("Fake Authentication", null);*/

// Auth0 Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
});
//

// Management API jwt token
builder.Services.AddAuth0AuthenticationClient(config =>
{
    config.Domain = builder.Configuration["Auth0:Authority"];
    config.ClientId = builder.Configuration["Auth0:ClientId"];
    config.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();
//

// Add claimsPrinciple
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext.User);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); //.RequireAuthorization();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{ // Require a DbContext from the service provider and seed the database.
    var dbContext = scope.ServiceProvider.GetRequiredService<VicDbContext>();
    var seeder = new Seeder(dbContext);
    seeder.Seed();
}

app.Run();

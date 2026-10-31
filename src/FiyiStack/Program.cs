using FiyiStack.Components;
using FiyiStack.DatabaseContexts;
using FiyiStack.Mailing;
using FiyiStack.Services.System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.Data.SqlClient;

//LOAD .ENV
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{

    var solutionDirectory = new DirectoryInfo(AppContext.BaseDirectory);

    while (solutionDirectory != null &&
           !File.Exists(Path.Combine(solutionDirectory.FullName, "FiyiStack.sln")))
    {
        solutionDirectory = solutionDirectory.Parent;
    }

    if (solutionDirectory == null)
    {
        throw new DirectoryNotFoundException("FiyiStack.sln was not found, so the development .env file could not be located.");
    }

    Env.Load(Path.Combine(solutionDirectory.FullName, ".env"));
}

var builder = WebApplication.CreateBuilder(args);

//ADD SERVICES FOR CONTROLLERS
builder.Services.AddControllers();

//ADD SERVICE FOR RAZOR COMPONENTS
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//ADD SERVICE TO USE DATABASE (SCOPED)
string DbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
if (string.IsNullOrWhiteSpace(DbConnectionString))
{
    throw new InvalidOperationException("Configure ConnectionStrings__DefaultConnection before starting the application.");
}

// Use the published port when running from the IDE; use the service name inside Compose.
if (builder.Environment.IsDevelopment() &&
    !string.Equals(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), "true", StringComparison.OrdinalIgnoreCase))
{
    var connection = new SqlConnectionStringBuilder(DbConnectionString);
    if (string.Equals(connection.DataSource, "fiyistack-db", StringComparison.OrdinalIgnoreCase))
    {
        connection.DataSource = "tcp:127.0.0.1,1433";
        DbConnectionString = connection.ConnectionString;
    }
}

builder.Services.AddDbContextFactory<FiyiStackDbContext>(options =>
    options.UseSqlServer(DbConnectionString));

//SCRUTOR
builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.Where(type =>
        type.Name.EndsWith("Repository") ||
        type.Name.EndsWith("ImportationService") ||
        type.Name.EndsWith("ExportationService")))
    .AsSelf()
    .WithScopedLifetime());

//ADD SCOPED SERVICE TO SEND EMAILS
builder.Services.AddScoped<Email>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddScoped<CookieService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDataProtection().UseEphemeralDataProtectionProvider();
}
else
{
    builder.Services.AddDataProtection();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/500", createScopeForErrors: true);
    //MORE INFO IN -> https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/404");

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var contextFactory = services.GetRequiredService<IDbContextFactory<FiyiStackDbContext>>();

    int maxRetries = 5;       // Maximum number of attempts
    int delayInSeconds = 5;  // Delay between attempts

    for (int retry = 1; retry <= maxRetries; retry++)
    {
        try
        {
            using var context = contextFactory.CreateDbContext();

            // Try to apply pending migrations
            context.Database.Migrate();

            // Migration succeeded; exit the loop.
            break;
        }
        catch (Exception ex)
        {
            if (retry == maxRetries)
            {
                // All attempts have been exhausted; throw an exception to stop the application.
                throw new Exception("No se pudo conectar a SQL Server después de varios intentos.", ex);
            }

            // Optional: Use a logger here instead of Console.WriteLine
            Console.WriteLine($"[Intento {retry}/{maxRetries}] SQL Server no está listo todavía. Reintentando en {delayInSeconds} segundos...");

            // Pause execution for a few seconds before the next attempt
            Thread.Sleep(TimeSpan.FromSeconds(delayInSeconds));
        }
    }
}

app.Run();

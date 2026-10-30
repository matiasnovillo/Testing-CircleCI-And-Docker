using FiyiStack.Components;
using FiyiStack.DatabaseContexts;
using FiyiStack.Mailing;
using FiyiStack.Services.System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

//ADD SERVICES FOR CONTROLLERS
builder.Services.AddControllers();

//ADD SERVICE FOR RAZOR COMPONENTS
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//ADD SERVICE TO USE DATABASE (SCOPED)
string DbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

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

    int maxRetries = 5;       // Número máximo de intentos
    int delayInSeconds = 5;  // Tiempo de espera entre intentos

    for (int retry = 1; retry <= maxRetries; retry++)
    {
        try
        {
            using var context = contextFactory.CreateDbContext();

            // Intenta aplicar las migraciones pendientes
            context.Database.Migrate();

            // Si llega aquí, la migración fue exitosa. Salimos del bucle.
            break;
        }
        catch (Exception ex)
        {
            if (retry == maxRetries)
            {
                // Si ya agotamos los intentos, lanzamos el error para que falle la app
                throw new Exception("No se pudo conectar a SQL Server después de varios intentos.", ex);
            }

            // Opcional: Puedes usar un Logger aquí en lugar de Console.WriteLine
            Console.WriteLine($"[Intento {retry}/{maxRetries}] SQL Server no está listo todavía. Reintentando en {delayInSeconds} segundos...");

            // Detiene la ejecución por unos segundos antes del siguiente intento
            Thread.Sleep(TimeSpan.FromSeconds(delayInSeconds));
        }
    }
}

app.Run();

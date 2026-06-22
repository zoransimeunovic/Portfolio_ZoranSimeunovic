using Microsoft.EntityFrameworkCore;
using SyncWorkerService;
using SyncWorkerService.Data;
using SyncWorkerService.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((ctx, config) =>
    {
        var env = ctx.HostingEnvironment.EnvironmentName;
        var sharedPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..");
        config.AddJsonFile(Path.Combine(sharedPath, "appsettings.json"), optional: true);
        config.AddJsonFile(Path.Combine(sharedPath, $"appsettings.{env}.json"), optional: true);
    })
    .ConfigureServices((ctx, services) =>
    {
        var conn = ctx.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' nije konfigurisan.");

        services.AddDbContext<SyncDbContext>(o =>
            o.UseMySql(conn, ServerVersion.AutoDetect(conn)));

        var clientId = ctx.Configuration["MicrosoftGraph:ClientId"]!;
        services.AddSingleton(_ => new MsGraphClient.MsGraphClient(clientId));
        services.AddScoped<EmailService>();
        services.AddScoped<LeadScannerService>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();

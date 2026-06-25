using Microsoft.EntityFrameworkCore;
using Serilog;
using SyncWorkerService;
using SyncWorkerService.Data;
using SyncWorkerService.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        Path.Combine(AppContext.BaseDirectory, "sync_worker.log"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .UseWindowsService()
    .ConfigureServices((ctx, services) =>
    {
        var conn = ctx.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' nije konfigurisan.");

        services.AddDbContext<SyncDbContext>(o =>
            o.UseMySql(conn, ServerVersion.AutoDetect(conn)));

        services.AddScoped<EmailService>();
        services.AddScoped<LeadScannerService>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();

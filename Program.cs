using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Localization;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "app.log"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' nije konfigurisan.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/login";
        options.LogoutPath = "/admin/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

var supportedCultures = new[]
{
    new CultureInfo("sr-Latn"),
    new CultureInfo("de"),
    new CultureInfo("en")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Default je srpski-latinica (kada nista drugo ne odgovara exYu posjetiocu);
    // za ostale, BrowserCultureProvider i fallback rjesavaju izbor.
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // Redoslijed providera (prvi koji vrati rezultat pobjedjuje):
    // 1) Cookie  -> rucni izbor korisnika ima prioritet
    // 2) Browser -> Accept-Language header
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    options.RequestCultureProviders.Add(new BrowserCultureProvider());
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(
    app.Services.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Migracija baze nije uspjela.");
    }
}

app.Run();

using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=portfolio.db";
var isMySql = connectionString.StartsWith("Server=", StringComparison.OrdinalIgnoreCase);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (isMySql)
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    else
        options.UseSqlite(connectionString);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/login";
        options.LogoutPath = "/admin/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

var graphClientId = builder.Configuration["MicrosoftGraph:ClientId"];
if (!string.IsNullOrWhiteSpace(graphClientId))
    builder.Services.AddSingleton(_ => new MsGraphClient.MsGraphClient(graphClientId));

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

var isConfigured = !string.IsNullOrWhiteSpace(connectionString) &&
                   !connectionString.Contains("Server=;");
if (isConfigured)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex,
            "Migracija baze nije uspela (provjeri connection string u appsettings.json).");
    }
}

app.Run();

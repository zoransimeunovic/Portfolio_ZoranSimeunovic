using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Localization;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// EF Core + MySQL (Pomelo)
// Eksplicitna verzija servera (bez AutoDetect) da se NE pokusava konekcija pri
// konfiguraciji - sajt se renderuje i prije nego sto se popuni connection string.
// Stvarni upit na bazu desava se tek pri slanju kontakt forme, gdje je zasticen
// try/catch-om. Promijeni MySqlServerVersion da odgovara tvom serveru ako treba.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Podrzane kulture
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

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Osiguraj da baza/tabela postoje - samo kada je connection string popunjen.
// Dok je placeholder (Server=;) preskacemo da izbjegnemo nepotreban timeout pri startu.
var isConfigured = !string.IsNullOrWhiteSpace(connectionString) &&
                   !connectionString.Contains("Server=;");
if (isConfigured)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex,
            "Baza nije inicijalizovana (provjeri connection string u appsettings.json).");
    }
}

app.Run();

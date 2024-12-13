using APS.Data;
using APS.Data.Models;
using APS.Security;
using APS.Web.Architecture;
using APS.Web.Filters;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using Rotativa.AspNetCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("es-CR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configura DbContext con SQL Server
builder.Services.AddDbContext<ApdatadbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging() // Para habilitar el logging de datos sensibles
           .LogTo(Console.WriteLine));   // Para enviar los logs a la consola

// Configuración de Azure Services
builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    // Azure Form Recognizer Client
    var formRecognizerEndpoint = configuration["Azure:FormRecognizer:Endpoint"];
    var formRecognizerKey = configuration["Azure:FormRecognizer:Key"];
    var formRecognizerCredential = new AzureKeyCredential(formRecognizerKey);
    return new DocumentAnalysisClient(new Uri(formRecognizerEndpoint), formRecognizerCredential);
});

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    // Configuración de Azure OpenAI
    var endpoint = new Uri(configuration["Azure:OpenAI:Endpoint"]);
    var apiKey = configuration["Azure:OpenAI:Key"];

    // Crear cliente de Azure OpenAI
    return new Azure.AI.OpenAI.AzureOpenAIClient(endpoint, new AzureKeyCredential(apiKey));
});
// Configura Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApdatadbContext>()
    .AddDefaultTokenProviders();

// Configura el inicio de sesión
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

// Agrega Razor Pages
builder.Services.AddRazorPages();

// Agrega controladores con vistas
builder.Services.AddControllersWithViews();

// Añadir Distributed Memory Cache para las sesiones
builder.Services.AddDistributedMemoryCache();

// Registro de IHttpClientFactory
builder.Services.AddHttpClient();

// Configurar y añadir el servicio de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Registra ISecurityService y SecurityService en el contenedor de servicios
builder.Services.AddScoped<ISecurityService, SecurityService>();

LocalConfiguration.Register(builder.Services);
RepositoryConfiguration.Register(builder.Services);
ServicesConfiguration.Register(builder.Services);

// Construye la aplicación
var app = builder.Build();



// Configura el pipeline de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Usar sesiones en la aplicación
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Configurar enrutamiento
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tienda}/{action=Index}/{id?}");

app.Run();
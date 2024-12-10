using APS.Data;
using APS.Data.Models;
using APS.Security;
using APS.Web.Architecture;
using APS.Web.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("es-CR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configura DbContext con SQL Server
builder.Services.AddDbContext<ApdatadbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.LogTo(Console.WriteLine);
    }
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
builder.Services.AddRazorPages();  // <-- Esto es clave para Razor Pages

// Agrega controladores con vistas
builder.Services.AddControllersWithViews();

// Añadir Distributed Memory Cache para las sesiones
builder.Services.AddDistributedMemoryCache();

// Configurar y añadir el servicio de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(128); // Tiempo de expiración de la sesión
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

// Configurar enrutamiento de Razor Pages
app.MapRazorPages();  // <-- Esto es clave para Razor Pages

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tienda}/{action=Index}/{id?}");

app.Run();
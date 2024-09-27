using APS.Data;
using APS.Data.Models;
using APS.Web.Filters;
using Microsoft.EntityFrameworkCore;

namespace APS.Web.Architecture;

internal static class RepositoryConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISecurityRepository, SecurityRepository>();
        serviceCollection.AddDbContext<ApdatadbContext>(options
            => options.UseSqlServer("Server=DESKTOP-HQR7UH5\\SQLEXPRESS;Database=APDatadb;Trusted_Connection=True;TrustServerCertificate=True;"));
    }
}

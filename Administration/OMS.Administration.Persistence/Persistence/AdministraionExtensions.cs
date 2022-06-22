using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMS.Administration.Infrasturcture.Persistence;
using OMS.Administration.Infrasturcture.Services;
using OMS.Administration.Infrasturcture.Services.Contracts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AdministraionDbContextExtensions
    {
        private const string MigrationAssemblyName = "OMS.Administration.Migrations";
        public static void AddAdministrationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("DefaultConnection");
            services.AddEntityFrameworkNpgsql().AddDbContext<AdministrationDbContext>(options =>
            {
                options.UseNpgsql(connectionString, x => x.MigrationsAssembly(MigrationAssemblyName));
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
            });
            services.AddTransient<IAdministrationDbContext, AdministrationDbContext>();
            services.AddTransient<IOrganizationService, OrganizationService>();
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMS.Extensions.Configuration.Vault;
using System;

namespace OMS.Administration.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string vaultUrl = Environment.GetEnvironmentVariable("VAULT_URL");
            string rootPassword = Environment.GetEnvironmentVariable("VAULT_ROOT_PWD");

            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureAppConfiguration(config => config.AddVaultConfiguration(() => new VaultOptions
            {
                VaultUri = vaultUrl,
                RootToken = rootPassword,
                BasePath = "OMSSettings",
                SubPathToUse ="Development"
            }));
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            return builder;
        }
    }
}

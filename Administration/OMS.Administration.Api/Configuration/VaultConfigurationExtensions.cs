using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.SecretsEngines;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class VaultConfigurationExtensions
    {
        public static IConfigurationBuilder AddVault(this IConfigurationBuilder builder)
        {
            string vaultUrl = Environment.GetEnvironmentVariable("VAULT_URL");
            string rootPassword = Environment.GetEnvironmentVariable("VAULT_ROOT_PWD");

            IAuthMethodInfo authInfo = new TokenAuthMethodInfo(rootPassword);
            VaultClientSettings settings = new VaultClientSettings(vaultUrl, authInfo);
            IVaultClient authenticatedVaultClient = new VaultClient(settings);

            var path = "OMSConfiguration/Development";
            var kv1SecretsEngine = new SecretsEngine
            {
                Type = SecretsEngineType.KeyValueV1,
                Path = path
            };

            var vaultSecrets = authenticatedVaultClient.V1.Secrets.KeyValue.V1.ReadSecretAsync(path, kv1SecretsEngine.Path).Result;
            var secrets = vaultSecrets.Data.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString()));
            builder.AddInMemoryCollection(secrets);
            return builder;
        }
    }
}

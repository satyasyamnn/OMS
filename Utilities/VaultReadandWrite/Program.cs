using System;
using System.Collections.Generic;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.SecretsEngines;

namespace VaultReadandWrite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAuthMethodInfo authInfo = new TokenAuthMethodInfo("myroot");
            VaultClientSettings settings = new VaultClientSettings("http://localhost:8200", authInfo);
            IVaultClient _authenticatedVaultClient = new VaultClient(settings);
            // RunKeyValueV1Sample(_authenticatedVaultClient);
            WriteToVaultV2Sample(_authenticatedVaultClient);
            RunKeyValueV2Sample(_authenticatedVaultClient);
            Console.Read();
        }

        private static void RunKeyValueV1Sample(IVaultClient _authenticatedVaultClient)
        {
            var path = "OMSConfiguration/Development";

            var kv1SecretsEngine = new SecretsEngine
            {
                Type = SecretsEngineType.KeyValueV1,
                Path = path
            };

            _authenticatedVaultClient.V1.System.MountSecretBackendAsync(kv1SecretsEngine).Wait();

            _authenticatedVaultClient.V1.Secrets.KeyValue.V1.DeleteSecretAsync(path).Wait();

            var values = new Dictionary<string, object>
            {
                {"DefaultConnection", "Server=localhost; Port = 5432; Database = omsdb; User Id = admin; Password = secret"},
                {"EnableDetailedErrors", true},
                {"EnableSensitiveDataLogging", true},
            };

            _authenticatedVaultClient.V1.Secrets.KeyValue.V1.WriteSecretAsync(path, values, kv1SecretsEngine.Path).Wait();
        }

        private static void RunKeyValueV2Sample(IVaultClient _authenticatedVaultClient)
        {
            var kv2SecretsEngine = new SecretsEngine
            {
                Type = SecretsEngineType.KeyValueV2,
                Path = "OMSSettings"
            };
            var paths = _authenticatedVaultClient.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync("", mountPoint: kv2SecretsEngine.Path).Result;
            foreach (string subPath in paths.Data.Keys)
            {
                var kv2Secret = _authenticatedVaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(subPath, mountPoint: kv2SecretsEngine.Path).Result;
                foreach(string key in kv2Secret.Data.Data.Keys)
                {
                    Console.WriteLine(kv2Secret.Data.Data[key]);
                }
            }
        }       
        

        private static void WriteToVaultV2Sample(IVaultClient _authenticatedVaultClient)
        {
            // mount a new v2 kv
            var kv2SecretsEngine = new SecretsEngine
            {
                Type = SecretsEngineType.KeyValueV2,
                Path = "OMSSettings"
            };
            _authenticatedVaultClient.V1.System.MountSecretBackendAsync(kv2SecretsEngine).Wait();
            string[] paths = new string[] { "Development", "Staging", "Production" };
            foreach (string path in paths)
            {               
                var values = new Dictionary<string, object> {
                    {"DefaultConnection", "Server=localhost; Port = 5432; Database = omsdb; User Id = admin; Password = secret"},
                    {"EnableDetailedErrors", true},
                    {"EnableSensitiveDataLogging", true}
                };
                string subPathToUse = path;
                var currentMetadata = _authenticatedVaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(subPathToUse, values, mountPoint: kv2SecretsEngine.Path).Result;
            }
        }
    }
}

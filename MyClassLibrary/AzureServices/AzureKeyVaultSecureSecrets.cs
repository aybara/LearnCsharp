using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace MyClassLibrary.AzureServices
{
    //Usar System.Configuration para acessar ConfigurationManager.AppSettings.Get(name), onde 'name' é o nome da chave no arquivo App.Config
    public class AzureKeyVaultSecureSecrets
    {
        private readonly SecretClient client;

        public AzureKeyVaultSecureSecrets()
        {
            string clientId = SecureSecrets.GetSecret("AzureKeyVaultClientId");
            string clientSecret = SecureSecrets.GetSecret("AzureKeyVaultClientSecret");
            string tenantId = SecureSecrets.GetSecret("AzureTranslatorTenantId");
            Uri keyVaultUri = new Uri(SecureSecrets.GetSecret("AzureKeyVaultUrl"));
            client = new SecretClient(keyVaultUri, new ClientSecretCredential(tenantId, clientId, clientSecret));
        }

        public string this[string secretName]
        {
            get { return Get(secretName); }
        }

        public async Task<string> GetAsync(string secretName)
        {
            Secret secret = await client?.GetAsync(secretName);
            return secret.Value;
        }
        public string Get(string secretName)
        {
            return GetAsync(secretName).GetAwaiter().GetResult();
        }
    }
}

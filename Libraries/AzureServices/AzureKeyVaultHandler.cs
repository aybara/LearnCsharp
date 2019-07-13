using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Libraries.AzureServices
{
    //Usar System.Configuration para acessar ConfigurationManager.AppSettings.Get(name), onde 'name' é o nome da chave no arquivo App.Config
    public class AzureKeyVaultHandler
    {
        
        private readonly string clientSecret;
        private readonly string clientId;
        private readonly string keyVaultUrl;
        public AzureKeyVaultHandler(string clientId, string clientSecret, string keyVaultUrl = "")
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.keyVaultUrl = keyVaultUrl;
        }
        public async Task<string> GetSecretAsync(string secretName, string keyVaultUrl = "")
        {
            if (keyVaultUrl == "")
                keyVaultUrl = this.keyVaultUrl;
            try
            {
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessTokenAsync));
                SecretBundle secret = await keyVaultClient.GetSecretAsync(keyVaultUrl, secretName).ConfigureAwait(false);
                return secret.Value;
            }
            catch(KeyVaultErrorException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
        private async Task<string> GetAccessTokenAsync(string authority, string resource, string scope)
        {
            var appCredentials = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var result = await context.AcquireTokenAsync(resource, appCredentials);
            return result.AccessToken;
        }
    }
}

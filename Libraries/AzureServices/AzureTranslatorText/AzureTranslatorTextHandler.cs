using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Libraries.AzureServices.AzureTranslatorText
{
    public partial class AzureTranslatorTextHandler
    {
        private readonly string host;
        private readonly string subscriptionKey;
        public AzureTranslatorTextHandler(string subscriptionKey, string host = "https://api.cognitive.microsofttranslator.com")
        {
            this.subscriptionKey = subscriptionKey;
            this.host = host;
        }
        public async Task<TranslationResult[]> TranslateTextRequestAsync(string inputText, string route)
        {
            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TranslationResult[]>(result);
            }
        }
        public async Task<TransliterationResult[]> TransliterateTextRequestAsync(string inputText, string route)
        {
            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                
                string result = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<TransliterationResult[]>(result);
            }
        }
        public async Task<DetectResult[]> DetectTextRequestAsync(string inputText, string route)
        {
            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                
                string result = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<DetectResult[]>(result);
            }
        }
    }
}

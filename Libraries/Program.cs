using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Libraries.AzureServices;
using Libraries.AzureServices.AzureTranslatorText;
using Libraries.Cache;
using Libraries.Queues;
using Libraries.Math;
using Newtonsoft.Json;

namespace Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            //AzureKeyVaultHandler azureKeyVault = new AzureKeyVaultHandler(
            //    ConfigurationManager.AppSettings.Get("AzureKeyVaultClientId"),
            //    ConfigurationManager.AppSettings.Get("AzureKeyVaultClientSecret"),
            //    ConfigurationManager.AppSettings.Get("AzureKeyVaultUrl"));

            //string secret = azureKeyVault.GetSecretAsync("HelloWorld").GetAwaiter().GetResult();

            AzureQueueProducerConsumer<string> azureQueue = new AzureQueueProducerConsumer<string>("DefaultEndpointsProtocol=https;AccountName=storageaccountqueues;AccountKey=XpJZXvRJhJRmEz4HkyxMeTZJ31v1qWHGztDiBAJsqgobjhNouaYkt1+DGd/TDobtsqRK/xJ+c6fPimgXRZh/7g==;EndpointSuffix=core.windows.net", "facebookqueue");
            
            var messages = azureQueue.queue.PeekMessages(32);
            List<JsonData> jsonDatas = new List<JsonData>();
            foreach (var message in messages)
            {
                jsonDatas.Add(JsonConvert.DeserializeObject<JsonData>(message.AsString));
            }
            jsonDatas.Where(data => data.Entry[0].Changed_Fields.Contains(""))
        }
    }
    public class JsonData
    {
        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }
    }
    public class Entry
    {
        [JsonProperty("changed_fields")]
        public List<string> Changed_Fields { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("time")]
        public int Time { get; set; }
    }

    public class Value
    {
        [JsonProperty("ad_id")]
        public string AdId { get; set; }

        [JsonProperty("form_id")]
        public string FormId { get; set; }

        [JsonProperty("leadgen_id")]
        public string LeadGenId { get; set; }

        [JsonProperty("created_time")]
        public int CreatedTime { get; set; }

        [JsonProperty("page_id")]
        public string PageId { get; set; }

        [JsonProperty("adgroup_id")]
        public string AdGroupId { get; set; }
    }

    public class LeadData
    {
        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("field_data")]
        public List<FieldData> FieldData { get; set; }
    }

    public class FieldData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public List<string> Values { get; set; }
    }

    public class LeadFormData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("leadgen_export_csv_url")]
        public string CsvExportUrl { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}

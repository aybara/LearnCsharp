using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Cache
{
    public class StockCache : ObjectCache
    {
        public CacheItemPolicy Policy { get; set; }
        private string FilePath = string.Empty;
        public StockCache(string fileName, CacheItemPolicy policy) : base()
        {
            Policy = policy;
            FilePath = fileName;
            if (!File.Exists(FilePath))
                File.Create(FilePath).Close();
        }

        #region Custom Methods
        private StockItem GetStockItem(string key)
        {
            List<StockItem> items = GetItemList();
            return items?.Where(item => item.Key == key).FirstOrDefault();
        }
        private void SetStockItem(string key, object value)
        {
            List<StockItem> items = GetItemList();
            using (FileStream stockFile = File.Create(FilePath))
            {
                if (items == null) items = new List<StockItem>();
                BinaryFormatter formatter = new BinaryFormatter();
                StockItem item = items?.Where(i => i.Key == key).FirstOrDefault();
                if (item != null)
                    item.Value = value;
                else items.Add(new StockItem(key, value));
                formatter.Serialize(stockFile, items);
            }
        }

        private List<StockItem> GetItemList()
        {
            using (FileStream stockFile = File.OpenRead(FilePath))
            {
                List<StockItem> items = null;
                if (stockFile.Length != 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    items = formatter.Deserialize(stockFile) as List<StockItem>;
                }
                return items;
            }
        }
        private StockItem RemoveStockItem(string key)
        {
            List<StockItem> items = GetItemList();
            StockItem item = items.Where(i => i.Key == key).FirstOrDefault();
            if(item != null)
            {
                items.Remove(item);
                using (FileStream stockFile = File.Create(FilePath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stockFile, items);
                }
                return item;
            }
            return null;
        }
        #endregion

        #region Override Methods
        public override object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value, Policy);
            }
        }
        public override string Name => string.Empty;
        public override DefaultCacheCapabilities DefaultCacheCapabilities
        {
            get
            {
                return DefaultCacheCapabilities.None;
            }
        }
        public override bool Add(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            StockItem item = GetStockItem(key);
            if (item != null)
            {
                if(item.Value == null || !item.Value.Equals(value))
                {
                    item.Value = value;
                    SetStockItem(key, value);
                }
                
                return false;
            }
            SetStockItem(key, value);
            return true;
        }
        public override bool Add(CacheItem item, CacheItemPolicy policy)
        {
            return Add(item.Key, item.Value, policy);
        }
        public override bool Add(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            return Add(key, value, policy);
        }
        public override object AddOrGetExisting(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            StockItem item = GetStockItem(key);
            if (item != null)
                return item;
            SetStockItem(key, value);
            return null;
        }
        public override CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy)
        {
            return AddOrGetExisting(value.Key, value.Value, policy) as CacheItem;
        }
        public override object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            return AddOrGetExisting(key, value, policy);
        }
        public override bool Contains(string key, string regionName = null)
        {
            return GetItemList().Where(item => item.Key == key).FirstOrDefault() != null;
        }
        public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys, string regionName = null)
        {
            throw new NotImplementedException();
        }
        public override object Get(string key, string regionName = null)
        {
            return GetStockItem(key);
        }
        public override CacheItem GetCacheItem(string key, string regionName = null)
        {
            return GetStockItem(key);
        }
        public override long GetCount(string regionName = null)
        {
            return GetItemList().Count();
        }
        protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            List<StockItem> items = GetItemList();
            List<KeyValuePair<string, object>> enumerator = new List<KeyValuePair<string, object>>(items.Count);
            foreach (StockItem item in items)
            {
                enumerator.Add(new KeyValuePair<string, object>(item.Key, item.Value));
            }
            return enumerator.GetEnumerator();
        }
        public override IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null)
        {
            return GetItemList()
                .Where(item => keys.Contains(item.Key))
                .ToDictionary(k => k.Key, v => v.Value);
        }
        public override IDictionary<string, object> GetValues(string regionName, params string[] keys)
        {
            return GetValues(keys);
        }
        public override object Remove(string key, string regionName = null)
        {
            return RemoveStockItem(key);
        }
        public override void Set(CacheItem item, CacheItemPolicy policy)
        {
            Add(item, policy);
        }
        public override void Set(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            Add(key, value, policy);
        }
        public override void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            Add(key, value, absoluteExpiration);
        }
        #endregion
    }
}

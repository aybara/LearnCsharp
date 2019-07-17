using System;
using System.Runtime.Caching;

namespace MyClassLibrary.Cache
{
    [Serializable]
    public class StockItem
    {
        public StockItem(string key, object value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public object Value { get; set; }

        public static implicit operator CacheItem(StockItem stockItem)
        {
            return new CacheItem(stockItem.Key, stockItem.Value);
        }
    }
}

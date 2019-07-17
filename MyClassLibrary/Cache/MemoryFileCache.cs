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
    public class MemoryFileCache : MemoryCache, IDisposable
    {
        private string FilePath = string.Empty;
        public MemoryFileCache(string fileName) : base(fileName)
        {
            FilePath = fileName;
            if (!File.Exists(FilePath))
                File.Create(FilePath).Close();
            else
            {
                FileContentsToCache();
            }
        }

        private void FileContentsToCache()
        {
            using (FileStream stream = File.OpenRead(FilePath))
            {
                if (stream.Length > 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //var teste = formatter.Deserialize(stream);
                    var items = (IEnumerator<KeyValuePair<string, object>>)formatter.Deserialize(stream);
                    while (items.MoveNext())
                    {
                        Add(items.Current.Key, items.Current.Value, new CacheItemPolicy());
                    }
                }
            }
        }
        public void CacheToFile()
        {
            var items = base.GetEnumerator();
            using (FileStream stream = File.Create(FilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, items);
            }
        }

        public new void Dispose()
        {
            CacheToFile();
            base.Dispose();
        }
    }
}

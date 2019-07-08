using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Libraries.Cache;

namespace Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MemoryFileCache cache = new MemoryFileCache("ola.txt"))
            {
                cache["Leandro2"] = "Ola";
            }
        }
    }
}

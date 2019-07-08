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
using Libraries.Math;

namespace Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            Matriz a = new Matriz(1, 1);
            Matriz b = new Matriz(1, 1);
            Matriz c = a + b;
        }
    }
}

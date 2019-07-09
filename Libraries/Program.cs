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
            int soma = 0;
            Teste((i, j) => soma += i + j);
        }

        public static void Teste(Action<int,int> action)
        {
            int k = 0;
            int i = 0, j = 0;
            while (k < 10)
            {
                action(i, j);
                k++; i++; j++;
            }
        }
    }
}

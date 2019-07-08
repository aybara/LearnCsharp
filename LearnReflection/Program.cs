using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LearnReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Pai pai = new Filho();
            pai.NomesPropriedades();
            var type = pai.GetType();
            Console.ReadLine();
        }
    }

    public class Pai
    {
        public string Nome { get; set; }
        
    }

    public interface

    public class Filho : Pai
    {
        public DateTime Nascimento { get; set; }
    }

    public class Filho2 : Pai
    {
        public string SobreNome { get; set; }
    }

    public static class MethodExtends
    {
        public static void NomesPropriedades(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                Console.WriteLine(property.Name);
            }
        }
    }
}

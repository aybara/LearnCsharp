using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.FactoryMethodAndAbstractFactory;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            FactoryMethodExamples();
            AbstractFactoryExamples("Stegosaurus");
            RandomFactoryExmples(new Random(DateTime.Now.Millisecond));
            Console.ReadLine();
        }
        public static void FactoryMethodExamples()
        {
            IDinosaurNoiseProducer fm1 = new TRexNoiseProducer();
            fm1.MakeADinosaurNoise();

            IDinosaurNoiseProducer fm2 = new StegosaurusNoiseProducer();
            fm2.MakeADinosaurNoise();
        }
        public static void AbstractFactoryExamples(string dinosaurType)
        {
            IDinosaurNoiseProducer af1 = new AbstractFactory.DinosaurNoiseProducer(new DinosaurFactory(dinosaurType));
            af1.MakeADinosaurNoise();
        }

        public static void RandomFactoryExmples(Random random)
        {
            RandomDinosaurFactory rf1 = new RandomDinosaurFactory(random);
            IDinosaur dinosaur = rf1.CreateADinosaur();
            dinosaur.MakeANoise();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethodAndAbstractFactory
{
    public class SingleDinosaurNoiseProducer : IDinosaurNoiseProducer
    {
        public void MakeADinosaurNoise()
        {
            IDinosaur dinosaur = new TRex();
            dinosaur.MakeANoise();
        }
    }

    public class TRex : IDinosaur
    {
        public void MakeANoise()
        {
            Console.WriteLine("RAWWR");
        }
    }

    public interface IDinosaurNoiseProducer
    {
        void MakeADinosaurNoise();
    }

    public interface IDinosaur
    {
        void MakeANoise();
    }
}

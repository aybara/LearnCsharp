using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethodAndAbstractFactory
{
    public class TRexNoiseProducer : IDinosaurNoiseProducer
    {
        public void MakeADinosaurNoise()
        {
            IDinosaur dinosaur = MakeADinosaur();

            dinosaur.MakeANoise();
        }

        public virtual IDinosaur MakeADinosaur()
        {
            return new TRex();
        }
    }
    public class StegosaurusNoiseProducer : TRexNoiseProducer
    {
        public override IDinosaur MakeADinosaur()
        {
            return new Stegosaurus();
        }
    }
    public class Stegosaurus : IDinosaur
    {
        public void MakeANoise()
        {
            Console.WriteLine("Squeak?");
        }
    }
}

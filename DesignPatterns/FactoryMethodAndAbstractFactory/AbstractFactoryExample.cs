using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethodAndAbstractFactory
{
    public static class AbstractFactory
    {
        public class DinosaurNoiseProducer : IDinosaurNoiseProducer
        {
            private IDinosaurFactory dinosaurFactory;

            public DinosaurNoiseProducer(IDinosaurFactory dinosaurFactory)
            {
                this.dinosaurFactory = dinosaurFactory;
            }

            public void MakeADinosaurNoise()
            {
                IDinosaur dinosaur = this.dinosaurFactory.CreateADinosaur();

                if (dinosaur != null)
                {
                    dinosaur.MakeANoise();
                }
            }
        }
    }
    public class DinosaurFactory : IDinosaurFactory
    {
        private readonly DinosaurType dinosaurType;

        public DinosaurFactory(string dinosaurType)
        {
            Enum.TryParse(dinosaurType, true, out this.dinosaurType);
        }

        public IDinosaur CreateADinosaur()
        {
            switch (this.dinosaurType)
            {
                case DinosaurType.TRex:
                    return new TRex();
                case DinosaurType.Stegosaurus:
                    return new Stegosaurus();
            }

            return null;
        }
    }

    public interface IDinosaurFactory
    {
        IDinosaur CreateADinosaur();
    }
    public enum DinosaurType
    {
        Unknown,
        TRex,
        Stegosaurus,
    }
}

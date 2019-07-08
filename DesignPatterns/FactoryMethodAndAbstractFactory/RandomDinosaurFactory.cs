using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.FactoryMethodAndAbstractFactory
{
    public class RandomDinosaurFactory : IDinosaurFactory
    {
        private Random random;

        public RandomDinosaurFactory(Random random)
        {
            this.random = random;
        }

        public IDinosaur CreateADinosaur()
        {
            int randomNumber = random.Next(0, 10);

            if (randomNumber < 5)
            {
                return new TRex();
            }
            else
            {
                return new Stegosaurus();
            }
        }
    }
}

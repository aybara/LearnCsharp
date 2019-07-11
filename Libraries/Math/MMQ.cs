using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libraries.Math;
using static System.Math;

namespace Libraries.Math
{
    /// <summary>
    /// Método dos Mínimos Quadrados
    /// </summary>
    public class MMQ
    {
        public Matriz Dados { get; }
        public MMQ(Matriz dados)
        {
            if(dados.Colunas == 4)
                Dados = dados;
        }

        public void Somatórias(int n)
        {
            if(Dados != null)
            {
                double[] somas = new double[2 * (n + 1)];
                
            }
        }
    }
}

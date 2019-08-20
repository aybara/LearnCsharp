using static System.Math;

namespace MyClassLibrary.Math
{
    public interface IFunction
    {
        double Value(double x);
    }

    public class Polinomio : IFunction
    {
        public double[] Parameters { get; }
        public Polinomio(double[] parameters)
        {
            Parameters = parameters;
        }
        public double Value(double x)
        {
            double y = 0.0;
            for (int i = 0; i < Parameters.Length; i++)
                y += Parameters[i] * Pow(x, i);

            return y;
        }
    }   
}

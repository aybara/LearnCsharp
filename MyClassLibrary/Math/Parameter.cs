namespace MyClassLibrary.Math
{
    public class Parameter
    {
        public double Value { get; set; }
        public double Error { get; set; }
        public Parameter() { }
        public Parameter(double value, double error = 0.0)
        {
            Value = value;
            Error = error;
        }
    }
}

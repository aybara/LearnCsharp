namespace MyClassLibrary.Math
{
    public class ExperimentalPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Error { get; set; }
        public ExperimentalPoint(double x, double y, double error)
        {
            X = x;
            Y = y;
            Error = error;
        }
    }
}

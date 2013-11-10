namespace Stanowisko.SharedClasses
{
    public class Sample
    {
        public double Value { get; private set; }

        public double Time { get; private set; }

        public Sample(double value, double time)
        {
            Value = value;
            Time = time;
        }
        
    }
}

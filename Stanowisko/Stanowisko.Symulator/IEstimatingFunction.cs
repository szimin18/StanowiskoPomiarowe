namespace Stanowisko.Symulator
{
    interface IEstimatingFunction
    {
        public double GetValue(double miliseconds, long sapmleInsertionDelay, long initialValue, long experimentDuration, long amplitude);
    }
}

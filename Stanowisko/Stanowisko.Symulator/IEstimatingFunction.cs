namespace Stanowisko.Symulator
{
    interface IEstimatingFunction
    {
        double GetValue(double miliseconds, long sampleInsertionDelay, long initialValue, long experimentDuration, long amplitude);
    }
}

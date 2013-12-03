using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.SharedClasses;


namespace Stanowisko.Calculator
{
    public class MeasurementCalculator : IMeasurementCalculator
    {
        private Measurement _measurement;
        private int _curveBeginning;
        private int _curveEnd;
        private double _coefficent;
        private IIntegratingModule _integrator;

        private void initializeBoundaries()
            //wyznaczam pozycje krzywej, uznając, że zaczyna się wtedy, kiedy wartości
            //poczynając od końców zmieniają się o więcej niż wyznaczony dośw. epsilon
        {
            //wartość wyznaczana doświadczalnie
            double epsilon = 0.01;

            
            List<Sample> samples = _measurement.GetSamples();

            double firstValue = samples.ElementAt(0).Value;
            _curveBeginning = 0;
            for (int i = 1; i < samples.Count; ++i)
            {
                if ((Math.Abs(samples.ElementAt(i).Value - firstValue)) > epsilon)
                {
                    _curveBeginning = i-1;
                    break;
                }
            }

            double lastValue = samples.ElementAt(samples.Count-1).Value;
            _curveEnd = samples.Count-1;
            for (int i = samples.Count-2; i > 0; --i)
            {
                if (Math.Abs(samples.ElementAt(i).Value - lastValue) > epsilon)
                {
                    _curveEnd = i+1;
                    break;
                }
            }
        }

        public MeasurementCalculator(Measurement measurements, IIntegratingModule integrator)
        {
            this._measurement = measurements;
            this.initializeBoundaries();
            this._integrator = integrator;
        }

        public int CurveBeginning
        {
            set
            {
                _curveBeginning = value;
            }

            get
            {
                return _curveBeginning;
            }
        }

        public int CurveEnd
        {
            set
            {
                _curveEnd = value;
            }

            get
            {
                return _curveEnd;
            }
        }

        public double Coefficent
        {
            set
            {
                _coefficent = value;
            }

            get
            {
                return _coefficent;
            }
        }

        public void Calibrate(double heat)
        {
            _coefficent=heat / _integrator.Integrate(_measurement.GetSamples(), CurveBeginning, CurveEnd);
        }

        public double CalculateHeat()
        {
            return _coefficent * _integrator.Integrate(_measurement.GetSamples(), CurveBeginning, CurveEnd);
        }
    }
}

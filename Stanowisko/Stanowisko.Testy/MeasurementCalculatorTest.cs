using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Collections.Generic;
using Stanowisko.SharedClasses;
using Stanowisko.Calculator;

namespace CalculatorTest
{
    [TestClass]
    public class ComputableMeasurementTests
    {
        [TestMethod]
        public void InitializeBoundariesOnlyCurve()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 10; ++i)
            {
                readings.Add(new Sample(i, 0.0));
            }
            Measurement firstMeasurement = new Measurement(readings);
            MeasurementCalculator firstTarget = new MeasurementCalculator(firstMeasurement, new TrapezoidalIntegratingModule());

            Assert.AreEqual(0, firstTarget.CurveBeginning);
            Assert.AreEqual(9, firstTarget.CurveEnd);

        }

        [TestMethod]
        public void InitializeBoundariesWithAdditionalSamples()
        {
            List<Sample> secondReadings = new List<Sample>();
            for (int i = 0; i < 4; ++i)
            {
                secondReadings.Add(new Sample(0, 0.0));
            }

            for (int i = 0; i < 10; ++i)
            {
                secondReadings.Add(new Sample((-i * (i - 10)), 0.0));
            }

            for (int i = 0; i < 2; ++i)
            {
                secondReadings.Add(new Sample(0, 0.0));
            }

            Measurement secondMeasurement = new Measurement(secondReadings);
            MeasurementCalculator secondTarget = new MeasurementCalculator(secondMeasurement, new TrapezoidalIntegratingModule());

            Assert.AreEqual(4, secondTarget.CurveBeginning);
            Assert.AreEqual(14, secondTarget.CurveEnd);
        }
    }
}

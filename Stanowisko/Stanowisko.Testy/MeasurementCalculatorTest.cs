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
            MeasurementCalculator firstTarget = new MeasurementCalculator(firstMeasurement);

            Assert.AreEqual(0, firstTarget.GetBoundaries().Item1);
            Assert.AreEqual(9, firstTarget.GetBoundaries().Item2);

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
            MeasurementCalculator secondTarget = new MeasurementCalculator(secondMeasurement);

            Assert.AreEqual(4, secondTarget.GetBoundaries().Item1);
            Assert.AreEqual(14, secondTarget.GetBoundaries().Item2);
        }
    }
}

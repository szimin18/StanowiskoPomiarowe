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
        public void TrapezoidalIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 11; ++i)
            {
                readings.Add(new Sample(i, 0.0));
            }
            Measurement firstMeasurement = new Measurement(readings);
            MeasurementCalculator firstTarget = new MeasurementCalculator(firstMeasurement);
            firstTarget.Coefficent = 1;

            Assert.AreEqual(firstTarget.CalculateHeat(), 50.0);

            List<Sample> secondReadings = new List<Sample>();
            for (int i = 0; i < 11; ++i)
            {
                secondReadings.Add(new Sample((-i * (i - 10)), 0.0));
            }
            Measurement secondMeasurement = new Measurement(secondReadings);
            MeasurementCalculator secondTarget = new MeasurementCalculator(secondMeasurement);
            secondTarget.Coefficent = 1;

            double epsilon = 2;
            double expected = 500.0 / 3.0;
            Assert.IsTrue(Math.Abs(secondTarget.CalculateHeat() - expected)<epsilon);
        }

        [TestMethod]
        public void InitializeBoundariesTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 10; ++i)
            {
                readings.Add(new Sample(i, 0.0));
            }
            Measurement firstMeasurement = new Measurement(readings);
            MeasurementCalculator firstTarget = new MeasurementCalculator(firstMeasurement);

            Assert.AreEqual(0, firstTarget.CurveBeginning);
            Assert.AreEqual(9, firstTarget.CurveEnd);

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

            Assert.AreEqual(4, secondTarget.CurveBeginning);
            Assert.AreEqual(14, secondTarget.CurveEnd);
        }
    }
}

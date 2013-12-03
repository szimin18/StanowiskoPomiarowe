using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.SharedClasses;
using Stanowisko.Calculator;
using System.Collections.Generic;

namespace Stanowisko.Testy
{
    [TestClass]
    public class IntegratingModuleTest
    {
        [TestMethod]
        public void TrapezoidalLineIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 1001; ++i)
            {
                readings.Add(new Sample(i, i));
            }
            IIntegratingModule target = new TrapezoidalIntegratingModule();

            Assert.AreEqual(target.Integrate(readings, 0, 1000), 500000.0);
        }

        [TestMethod]
        public void TrapezoidalParabolaIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 1001; ++i)
            {
                readings.Add(new Sample((-i * (i - 1000)), i));
            }
            IIntegratingModule target = new TrapezoidalIntegratingModule();

            double epsilon = 200;
            double expected = 500000000.0 / 3.0;
            double result = target.Integrate(readings, 0, 1000);
            Assert.IsTrue(Math.Abs(result - expected) < epsilon);
        }

        [TestMethod]
        public void SimpsonsLineIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 1001; ++i)
            {
                readings.Add(new Sample(i, i));
            }
            IIntegratingModule target = new SimpsonsIntegratingModule();

            Assert.AreEqual(target.Integrate(readings, 0, 1000), 500000.0);
        }

        [TestMethod]
        public void SimpsonsParabolaIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 1001; ++i)
            {
                readings.Add(new Sample((-i * (i - 1000)), i));
            }
            IIntegratingModule target = new SimpsonsIntegratingModule();

            double epsilon = 2;
            double expected = 500000000.0 / 3.0;
            Assert.IsTrue(Math.Abs(target.Integrate(readings, 0, 1000) - expected) < epsilon);
        }
    }
}

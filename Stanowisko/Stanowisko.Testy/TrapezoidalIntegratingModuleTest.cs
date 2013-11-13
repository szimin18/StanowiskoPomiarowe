using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.SharedClasses;
using Stanowisko.Calculator;
using System.Collections.Generic;

namespace Stanowisko.Testy
{
    [TestClass]
    public class TrapezoidalIntegratingModuleTest
    {
        [TestMethod]
        public void LineIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 11; ++i)
            {
                readings.Add(new Sample(i, i));
            }
            TrapezoidalIntegratingModule target = new TrapezoidalIntegratingModule();

            Assert.AreEqual(target.Integrate(readings, 0, 10), 50.0);
        }

        [TestMethod]
        public void ParabolaIntegrationTest()
        {
            List<Sample> readings = new List<Sample>();
            for (int i = 0; i < 11; ++i)
            {
                readings.Add(new Sample((-i * (i - 10)), i));
            }
            TrapezoidalIntegratingModule target = new TrapezoidalIntegratingModule();

            double epsilon = 2;
            double expected = 500.0 / 3.0;
            Assert.IsTrue(Math.Abs(target.Integrate(readings, 0, 10) - expected) < epsilon);
        }
    }
}

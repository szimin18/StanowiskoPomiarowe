using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Symulator;

namespace Stanowisko.Testy
{
    [TestClass]
    public class SymulatorTesty
    {
        [TestMethod]
        public void ConnectingTest()
        {
            IMeasuringDevice simulator = new Simulator();
        }
    }
}

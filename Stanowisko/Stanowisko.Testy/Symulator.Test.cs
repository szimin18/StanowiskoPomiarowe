using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Symulator;

namespace Stanowisko.Testy
{
    [TestClass]
    public class SimulatorTests
    {
        //checks for validity of opening connection when it is opened or closed
        [TestMethod]
        public void ConnectingTest()
        {
            Simulator simulator = new Simulator();
            Assert.AreEqual(simulator.StartConnection(), null);
            Assert.AreEqual(simulator.StartConnection(), "Połączenie już istnieje");
        }

        //checks for validity of multiple following reqests to to stop connection when it is opened or closed
        [TestMethod]
        public void DisconnectingTest()
        {
            Simulator simulator = new Simulator();
            simulator.StopConnection();
            simulator.StartConnection();
            simulator.StopConnection();
            simulator.StopConnection();
        }
    }
}

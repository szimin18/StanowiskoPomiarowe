using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Symulator;

namespace Stanowisko.Testy
{
    [TestClass]
    public class SimulatorTests
    {
        private Simulator _simulator;

        [TestInitialize]
        public void InitializeSimulator()
        {
            _simulator = new Simulator();
        }

        //checks for validity of opening connection when it is opened or closed
        [TestMethod]
        public void ConnectingTest()
        {
            Assert.AreEqual(_simulator.StartConnection(), null);
            Assert.AreEqual(_simulator.StartConnection(), "Połączenie już istnieje");
        }

        //checks for validity of multiple following reqests to to stop connection when it is opened or closed
        [TestMethod]
        public void DisconnectingTest()
        {
            _simulator.StopConnection();
            _simulator.StartConnection();
            _simulator.StopConnection();
            _simulator.StopConnection();
        }

        [TestMethod]
        public void GettingSampleFromDisconnectedDeviceTest()
        {
            Assert.Fail("Połączenie nie istnieje");
            _simulator.GetSample();
        }
    }
}

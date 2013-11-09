using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stanowisko.Symulator;
using System;

namespace Stanowisko.Testy
{
    [TestClass]
    public class SimulatorTests
    {
        private IMeasuringDevice _simulator;

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

        //shows that attempt to get sample from disconnected device fails
        [TestMethod]
        [ExpectedException(typeof(Exception), "Połączenie nie istnieje")]
        public void GettingSampleFromDisconnectedDeviceTest()
        {
            _simulator.GetSample();
        }

        //shows that mane attempts to get sample from connected device succeed
        [TestMethod]
        public void GettingSampleFromConnectedDeviceTest()
        {
            _simulator.StartConnection();
            for (int i = 0; i < 10000; i++)
            {
                _simulator.GetSample();
            }
        }
    }
}
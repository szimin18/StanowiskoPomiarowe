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

        //checks for validity of opening connection
        [TestMethod]
        public void StartConnectionTest1()
        {
            Assert.AreEqual(_simulator.StartConnection(), null);
        }

        //checks for validity of opening connection when it is already opened
        [TestMethod]
        public void StartConnectionTest2()
        {
            Assert.AreEqual(_simulator.StartConnection(), null);
            Assert.AreEqual(_simulator.StartConnection(), "Połączenie już istnieje");
        }

        //checks for validity of reqests to stop connection when it is closed
        [TestMethod]
        public void StopConnectionTest1()
        {
            _simulator.StopConnection();
        }

        //checks for validity of reqests to stop connection when it is opened
        [TestMethod]
        public void StopConnectionTest1()
        {
            _simulator.StartConnection();
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
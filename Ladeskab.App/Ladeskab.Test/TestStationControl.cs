using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab;
using Ladeskab.Core.Library.Classes;
using Ladeskab.Core.Library.Interfaces;
using NSubstitute;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestStationControl
    {

        private StationControl _uut;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _rfid;
        private IChargeControl _chargeControl;

        [SetUp]
        public void Setup()
        {

            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _rfid = Substitute.For<IRfidReader>();
            _chargeControl = Substitute.For<IChargeControl>();

            _uut = new StationControl(_door, _display, _rfid, _chargeControl);
        }


        [Test]
        public void TestRfidDetected_Available_checkLockDoor()
        {
            _chargeControl.isConnected().Returns(true);

            _rfid.RfidEvent += Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

            _door.Received().LockDoor(); // spørg Frank i morgen
        }


        [Test]
        public void TestRfidDetected_Available_checkStartCharge()
        {
            _chargeControl.isConnected().Returns(true);

            _rfid.RfidEvent += Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

            _chargeControl.Received().StartCharge();
        }


        [Test]
        public void TestRfidDetected_Locked_testStopCharging()
        {
            //Act
            _chargeControl.isConnected().Returns(true);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

                //Assert
            _chargeControl.Received().StopCharge();
        }


        [Test]
        public void TestRfidDetected_Available_checkOldId()
        {
            //Assert
            _chargeControl.isConnected().Returns(true);

            //Act
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

            //Assert
            Assert.That(_uut.OldId, Is.EqualTo(1234));
        }

        [Test]
        public void TestRfidDetected_Locked_checkOldId()
        {
            //Assert
            _chargeControl.isConnected().Returns(true);

            //Act
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

            //Assert
            Assert.That(_uut.OldId, Is.EqualTo(1234));
        }

    }
}
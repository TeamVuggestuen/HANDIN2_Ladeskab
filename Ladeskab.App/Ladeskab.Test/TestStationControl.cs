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
        public void TestDoorStateChangedDetected_Available_DoorClosedFalse_Display()
        {
            //Assert
            // _chargeControl.isConnected().Returns(false);

            //Act
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = false });

            _display.Received().displayCommands("Connect phone (and close door(press 'C'))");
        }


        [Test]
        public void TestDoorStateChangedDetected_Available_DoorClosedTrue_Display()
        {
            //Assert
            // _chargeControl.isConnected().Returns(false);

            //Act
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = true });

            _display.Received().displayCommands("Error");
        }


        [Test]
        public void TestDoorStateChangedDetected_DoorOpen_DoorClosedTrue_Display()
        {
            //Assert
            // _chargeControl.isConnected().Returns(false);

            //Act
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = false });
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = true });

            _display.Received().displayCommands("Read rfid (press 'R')");
        }


        [Test]
        public void TestDoorStateChangedDetected_DoorOpen_DoorClosedFalse_Display()
        {
            //Assert
            // _chargeControl.isConnected().Returns(false);
            //Act
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = false });
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = false });

            _display.Received().displayCommands("Error");
        }


        [Test]
        public void TestDoorStateChangedDetected_DoorLockedFalse_Display()
        {
            //Assert
            _chargeControl.isConnected().Returns(true);
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });
            //Act
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = false });

            _display.Received().displayCommands("Locker is occupied");
        }


        [Test]
        public void TestDoorStateChangedDetected_DoorLockedTrue_Display()
        {
            //Assert
            _chargeControl.isConnected().Returns(true);
            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });
            //Act
            _door.DoorEvent +=
                Raise.EventWith(new DoorEventArgs() { DoorClosed = true });

            _display.Received().displayCommands("Locker is occupied");
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


        [Test]
        public void TestRfidDetected_Available_checkNotConnected()
        {
            _chargeControl.isConnected().Returns(false);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

            _display.Received().displayCommands("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }


        [Test]
        public void TestRfidDetected_Locked_WrongRfidTag()
        {
            _chargeControl.isConnected().Returns(true);

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 1234 });

            _rfid.RfidEvent +=
                Raise.EventWith(new RfidEventArgs() { Rfid_ID = 4321 });

            Assert.That(_uut.OldId, Is.Not.EqualTo(4321));
            //_display.Received().displayCommands("Forkert RFID tag.");
        }
    }
}
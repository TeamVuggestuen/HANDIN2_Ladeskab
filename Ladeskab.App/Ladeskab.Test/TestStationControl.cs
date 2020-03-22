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
        private Door _door;
        private Display _display;
        private RfidReader _rfidReader;
        private UsbChargerSimulator _usbChargerSimulator;
        private ChargeControl _chargeControl;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<Door>();
            _display = Substitute.For<Display>();
            _rfidReader = Substitute.For<RfidReader>();
            _usbChargerSimulator = Substitute.For<UsbChargerSimulator>();
            _chargeControl = new ChargeControl(_usbChargerSimulator, _display);
            _uut = new StationControl(_door, _display, _rfidReader, _chargeControl);
        }

        [Test]
        public void Ladeskab_teststate()
        {
            //Arrange
                //Setup
        _uut.
            //Act
            _door.OnDoorOpen();
            

               //Assert
            Assert.That(_display.Text, Is.EqualTo("Connect phone (and close door(press 'r'))"));

        }





    }
}
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab;
using NSubstitute;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IDisplay _display;
        private IRFIDReader _rfidReader;
        private UsbChargerSimulator _usbChargerSimulator;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _usbChargerSimulator = Substitute.For<UsbChargerSimulator>();

            _uut = new StationControl(_door, _display, _rfidReader, _usbChargerSimulator);
        }

        //[Test]
        //public void Ladeskab_teststate()
        //{
        //    Assert.That(_uut. Is.False);
        //}





    }
}
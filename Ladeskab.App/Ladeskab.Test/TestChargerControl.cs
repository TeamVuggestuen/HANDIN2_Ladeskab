using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Core.Library.Classes;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestChargerControl
    {
        private ChargeControl _uut;
        private IDisplay _display;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();

            _uut = new ChargeControl(_usbCharger, _display);
        }

        [Test]
        public void TestHandleChargerEvent_ConnectedIsTrue_WhenCurrentIsZero()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 0 });
            
            //Assert
            Assert.That(_uut.Connected, Is.EqualTo(false));
        }

    }
}

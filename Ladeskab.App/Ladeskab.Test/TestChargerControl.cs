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
        public void TestHandleChargerEvent_Disconnected()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 0 });
            
            //Assert
            Assert.That(_uut.Connected, Is.EqualTo(false));
        }


        [Test]
        public void TestHandleChargerEvent_FullyCharged_NoCurrent_NoDisplayAction()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 0 });

            //Assert
            _display.DidNotReceive().displayCommands(_uut.Chargedmessage);
            //Assert.That(_uut.Connected, Is.EqualTo(false));
        }


        [Test]
        public void TestHandleChargerEvent_FullyCharged_LowerBorderCurrent_Display()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 0.5 });

            //Assert
            _display.Received().displayCommands(_uut.Chargedmessage);
            //Assert.That(_uut.Connected, Is.EqualTo(false));
        }

        [Test]
        public void TestHandleChargerEvent_FullyCharged_UpperBorderCurrent_Display()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 4.5 });

            //Assert
            _display.Received().displayCommands(_uut.Chargedmessage);
            //Assert.That(_uut.Connected, Is.EqualTo(false));
        }


        [Test]
        public void TestHandleChargerEvent_FullyCharged_EqualToUpperBorderCurrent_Display()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 5 });

            //Assert
            _display.Received().displayCommands(_uut.Chargedmessage);
            //Assert.That(_uut.Connected, Is.EqualTo(false));
        }


        [Test]
        public void TestHandleChargerEvent_FullyCharged_UpperBorderCurrentNotValid_NoDisplay()
        {
            //Arrange
            //Act
            _usbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs() { Current = 5.5 });

            //Assert
            _display.DidNotReceive().displayCommands(_uut.Chargedmessage);
            //Assert.That(_uut.Connected, Is.EqualTo(false));
        }


        [TestCase(true)]
        [TestCase(false)]
        public void TestIsConnected(bool Connected)
        {
            _usbCharger.Connected.Returns(Connected);
            Assert.That(_uut.isConnected, Is.EqualTo(Connected));
        }


        [Test]
        public void TestStartCharge()
        {
            _uut.StartCharge();

            _usbCharger.Received().StartCharge();
        }

        [Test]
        public void TestStopCharge()
        {
            _uut.StopCharge();

            _usbCharger.Received().StopCharge();
        }

    }
}

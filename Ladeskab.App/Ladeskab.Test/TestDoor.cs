using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;


namespace Ladeskab.Test
{
    [TestFixture]
    public class TestDoor
    {
        public Door _uut;
        
        //public EventArgs _receivedEventArgs;
        //
        //public StationControl _Control;

        [SetUp]
        public void Setup()
        {
            _uut = new Door();

            ////Tjekker på events
            //_receivedEventArgs = null;

            //_uut.DoorEvent +=
            //    (o, args) =>
            //    {
            //        _receivedEventArgs = args;
            //    };

        }

        [Test]
        public void ctor_DoorIsUnLocked()
        {
            //Arrange
                //Setup

            //Act

            //Assert
            Assert.That(_uut.doorIsLocked, Is.False);
        }

        [Test]
        public void ctor_DoorIsClosed()
        {
            //Arrange
                //Setup

            //Act

            //Assert
            Assert.That(_uut.doorIsClosed, Is.True);
        }

        [Test]
        public void CheckOnDoorIsOpen()
        {
            //Arrange
                //Setup

            //Act
            _uut.OnDoorOpen();
            
            //Assert
            Assert.That(_uut.doorIsLocked, Is.False);

        }

        [Test]
        public void CheckOnDoorIsClose()
        {
            //Arrange
                //Setup

            //Act
            _uut.OnDoorClosed();
            _uut.doorIsLocked = true;
            
            //Assert
            Assert.That(_uut.doorIsLocked, Is.True);

        }

        [Test]
        public void OnDoorOpen_Event_Counter()
        {
            //Arrange
            int numValues = 0;

            //Act
            _uut.DoorEvent += (o, args) => numValues++;

            _uut.OnDoorOpen();
            _uut.OnDoorOpen();
            _uut.OnDoorOpen();

            //Assert
            Assert.That(numValues, Is.EqualTo(3));
        }

        [Test]
        public void OnDoorClosed_Event_Counter()
        {
            //Arrange
            int numValues = 0;

            //Act
            _uut.DoorEvent += (o, args) => numValues++;

            _uut.OnDoorClosed();
            _uut.OnDoorClosed();
            _uut.OnDoorClosed();

            //Assert
            Assert.That(numValues, Is.EqualTo(3));
        }

        [Test]
        public void lockDoor_doorIsLocked()
        {
            //Arrange
                //Setup

            //Act
            _uut.LockDoor();

            //Assert
            Assert.That(_uut.doorIsLocked, Is.True);
        }

        [Test]
        public void unlockDoor_doorIsUnLocked()
        {
            //Arrange
                //Setup
            
            //Act    
            _uut.UnlockDoor();
            
            //Assert
            Assert.That(_uut.doorIsLocked, Is.False);
        }

        //[Test]
        //public void DoorClosed_Get_status()
        //{
        //    //Arrange
        //        IDoor _uut = new Door();

        //    //Act    
        //    _uut.

        //    //Assert
        //    Assert.That(_uut.doorIsLocked, Is.False);
        //}

        //[Test]
        //public void CheckOnDoorIsOpenEvent()
        //{

        //    // Arrange
        //    _uut = Substitute.For<Door>();

        //    //Act
        //    var wasCalledClose = false;
        //    _uut.DoorEvent += (sender, args) => wasCalledClose = true;
        //    _uut.DoorEvent += Raise.EventWith(new DoorEventArgs()); // Kan ikke invoke vores event sådan.. 

        //    //Assert
        //    Assert.True(wasCalledClose);
        //}
    }


}
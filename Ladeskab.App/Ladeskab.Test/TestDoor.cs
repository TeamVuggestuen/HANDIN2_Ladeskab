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
        private DoorEventArgs _recievedDoorEventArgs;

       [SetUp]
        public void Setup()
        {
            _recievedDoorEventArgs = null;
            _uut = new Door();

            _uut.DoorEvent += (o, args) =>
            {
                _recievedDoorEventArgs = args;
            };
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
            Assert.That(_uut.doorIsClosed, Is.False);
        }

        [Test]
        public void CheckOnDoorIsOpenWhenOpen()
        {
            //Arrange
            //Setup

            //Act
            _uut.doorIsClosed = false;
            _uut.OnDoorOpen();

            //Assert
            Assert.That(_uut.doorIsLocked, Is.False);
        }


        [Test]
        public void CheckOnDoorIsClosed()
        {
            //Arrange
                //Setup

            //Act
            _uut.OnDoorClosed();

            //Assert
            Assert.That(_uut.doorIsClosed, Is.True);
        }


        [Test]
        public void CheckOnDoorIsClosedWhenClosed()
        {
            //Arrange
            //Setup

            //Act
            _uut.doorIsClosed = true;
            _uut.OnDoorClosed();

            //Assert
            Assert.That(_uut.doorIsClosed, Is.True);
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
        public void CheckOnSendArg_OnOpenDoor()
        {
            _uut.OnDoorOpen();
            Assert.That(_recievedDoorEventArgs.DoorClosed,
                Is.EqualTo(false));
        }


        [Test]
        public void CheckOnSendArg_OnOpenClosed()
        {
            _uut.OnDoorClosed();
            Assert.That(_recievedDoorEventArgs.DoorClosed,
                Is.EqualTo(true));
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

       
    }


}
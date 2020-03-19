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
        public EventArgs _receivedEventArgs;
        public StationControl _Control;
        public IDoor _IDoor;

        [SetUp]
        public void Setup()
        {
            _uut = new Door();

            //Tjekker på events
            _receivedEventArgs = null;

            _uut.DoorEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

        }

        [Test]
        public void ctor_DoorIsUnLocked()
        {
            Assert.That(_uut.doorIsLocked, Is.False);
        }

        [Test]
        public void ctor_DoorIsClosed()
        {
            Assert.That(_uut.doorIsClosed, Is.True);
        }

        [Test]
        public void CheckOnDoorIsOpen()
        {
            _uut.OnDoorOpen();

            Assert.That(_uut.doorIsLocked, Is.False);

        }

        [Test]
        public void CheckOnDoorIsClose()
        {
            _uut.OnDoorClosed();
            _uut.doorIsLocked = true;
            Assert.That(_uut.doorIsLocked, Is.True);

        }

        [Test]
        public void OnDoorOpen_Event_Counter()
        {
            int numValues = 0;
            _uut.DoorEvent += (o, args) => numValues++;

            _uut.OnDoorOpen();
            _uut.OnDoorOpen();
            _uut.OnDoorOpen();

            Assert.That(numValues, Is.EqualTo(3));
        }

        [Test]
        public void OnDoorClosed_Event_Counter()
        {
            int numValues = 0;
            _uut.DoorEvent += (o, args) => numValues++;

            _uut.OnDoorClosed();
            _uut.OnDoorClosed();
            _uut.OnDoorClosed();

            Assert.That(numValues, Is.EqualTo(3));
        }

        [Test]
        public void CheckOnDoorEvent()
        {

            // Arrange
            _IDoor = Substitute.For<IDoor>();

            //Act
            var EventInvoke = false;
            _IDoor.DoorEvent += (o, args) => EventInvoke = true;
            _IDoor.DoorEvent += Raise.EventWith(new DoorEventArgs()); // Kan ikke invoke vores event sådan.. 

            //Assert
            Assert.True(EventInvoke);
        }

        //[Test]
        //public void CheckOnDoorIsOpenEvent()
        //{

        //    // Arrange
        //    _IDoor = Substitute.For<IDoor>();

        //    //Act
            
        //    _IDoor.LockDoor();
            


        //    //Assert
        //    Assert.True(LockDoor());
        //}


        
    }



}
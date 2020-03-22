using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestRfidReader
    {

        public RfidReader _uut;
        public IRfidReader Reader;


        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();

        }

        [Test]
        public void RfidReader_event_OnRfidDetected()
        {
            //Arrange
            int numValues = 0;
            _uut.RfidEvent += (o, args) => numValues++;
            
            //Act    
            _uut.onRfidRead(123);
            _uut.onRfidRead(123);
            _uut.onRfidRead(123);

            //Assert
            Assert.That(numValues, Is.EqualTo(3));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    class TestDisplay
    {
        private Display _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [Test]
        public void CheckDisplayCommand()
        { 
            _uut.displayCommands("SWT");
            Assert.That(_uut.Text, Is.EqualTo("SWT"));
        }
    }
}

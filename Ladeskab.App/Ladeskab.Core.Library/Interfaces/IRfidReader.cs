using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    class IRfidReader
    {
        public class RfidEventArgs : EventArgs
        {
            public int Rfid_ID { get; set; }
        }

        public interface IRFIDReader
        {
            event EventHandler<RfidEventArgs> RfidEvent;
            void onRfidRead(int id);
        }
    }
}

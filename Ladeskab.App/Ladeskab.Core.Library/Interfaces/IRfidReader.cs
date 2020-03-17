using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class RfidEventArgs : EventArgs
    {
        public int Rfid_ID { get; set; }
    }

    public interface IRfidReader
    {
        event EventHandler<RfidEventArgs> RfidEvent;
        void onRfidRead(int id);
    }
}

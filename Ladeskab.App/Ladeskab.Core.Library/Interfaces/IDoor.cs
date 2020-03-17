using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.App;
using Ladeskab;

namespace Ladeskab
{
    public class DoorEventArgs : EventArgs
    {
        public bool DoorClosed { get; set; }
    }

    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorEvent;
        void UnlockDoor();
        void LockDoor();
    }
}

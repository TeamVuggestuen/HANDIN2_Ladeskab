using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;

namespace Ladeskab
{
    public class DoorEventArgs : EventArgs
    {
        private bool _doorClosed;

        public bool DoorClosed
        {
            set { _doorClosed = value; }
            get { return _doorClosed; }
        }
    }

    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorEvent;
        void UnlockDoor();
        void LockDoor();
    }
}

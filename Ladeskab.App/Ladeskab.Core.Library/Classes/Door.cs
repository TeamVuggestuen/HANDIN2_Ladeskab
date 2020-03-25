using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;

namespace Ladeskab
{
    public class Door : IDoor
    {
        // need a display to communicate to user
        //private IDisplay _display;


        //door states. Locked is controlled by stationcontrol. Closed is controlled by user (no need for interface description).
        public bool doorIsLocked;
        public bool doorIsClosed;


        //event handling
        public event EventHandler<DoorEventArgs> DoorEvent;
        protected virtual void OnDoorChanged(DoorEventArgs e)
        {
            DoorEvent?.Invoke(this, e);
        }


        //constructor (Door is closed but unlocked)
        public Door()
        {
            doorIsLocked = false;
            doorIsClosed = true;
        }


        //member function to open/close door
        #region open/close functions

        public void OnDoorOpen()
        {
            doorIsClosed = false;
            OnDoorChanged(new DoorEventArgs { DoorClosed = doorIsClosed });
        }

        public void OnDoorClosed()
        {
            doorIsClosed = true;
            OnDoorChanged(new DoorEventArgs { DoorClosed = doorIsClosed });
        }

        #endregion


        // member functions for locking/unlocking door
        #region lock/unlock functions

        public void UnlockDoor()
        {
            doorIsLocked = false;
        }

        public void LockDoor() 
        {
            doorIsLocked = true;
        }

        #endregion
    }
}

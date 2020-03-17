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
        private IDisplay _display;


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

            #region alt

            //if (doorIsLocked)
            //{
            //    Console.WriteLine("Door is locked. Scan rfid to unlock"); // bliver håndteret i station control og på denne måde skal vi ikke kommunikere med display fra døren
            //}
            //else if (!doorIsClosed) // kan evt fjernes (selvindlysende)
            //{
            //    Console.WriteLine("You're trying to open an open door");
            //}
            //else if (!doorIsLocked)
            //{
            //    doorIsClosed = false;
            //    OnDoorChanged(new DoorEventArgs { DoorClosed = doorIsClosed });
            //}            

            #endregion
        }

        public void OnDoorClosed()
        {
            doorIsClosed = true;
            OnDoorChanged(new DoorEventArgs { DoorClosed = doorIsClosed });

            #region alt

            //if (doorIsClosed) // kan evt fjernes (selvindlysende)
            //{
            //    Console.WriteLine("You're trying to close a closed door...");
            //}
            //else
            //{
            //    doorIsClosed = true;
            //    OnDoorChanged(new DoorEventArgs { DoorClosed = doorIsClosed });
            //}            

            #endregion
        }


        #endregion


        // member functions for locking/unlocking door
        // DISSE 2 FUNKTIONER ER KUN MED TIL AT VISE HVAD BRUGEREN ER I GANG MED, OG KALDER DERFOR VIDERE
        #region lock/unlock functions

        public void UnlockDoor()
        {
            doorIsLocked = true;
        }

        public void LockDoor() // fjern evt if sætningen // nemmere at teste
        {
            doorIsLocked = true;
        }

        #endregion
    }
}

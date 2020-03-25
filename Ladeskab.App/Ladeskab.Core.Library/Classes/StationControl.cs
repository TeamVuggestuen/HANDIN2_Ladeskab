using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ladeskab.Core.Library.Interfaces;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // member variables
        private LadeskabState _state;
        private IChargeControl _chargeControl;
        private IDoor _Door;
        private IDisplay _Display;
        
        //private int _oldId { get; set; }
        
        private int _oldId;

        public int OldId
        {
            get { return _oldId; }
            private set { _oldId = value; }
        }



        private string logFile = "logfile.txt"; // Navnet på systemets log-fil


        public StationControl(IDoor Door, IDisplay display, IRfidReader RfidReader, IChargeControl chargeControl)
        {

            Door.DoorEvent += HandleDoorEvent;                  //attach to door event
            _Door = Door;

            RfidReader.RfidEvent += HandleRfidEvent;            //attach to rfid event 

            _Display = display;

            _chargeControl = chargeControl;

            _state = LadeskabState.Available;
        }



        private void HandleDoorEvent(object sender, DoorEventArgs e)
        {
            doorStateChangeDetected(e);
        }

        private void doorStateChangeDetected(DoorEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (!e.DoorClosed)
                    {
                        _Display.displayCommands("Connect phone (and close door(press 'C'))");
                        _state = LadeskabState.DoorOpen;
                    }
                    else
                    {
                        _Display.displayCommands("Error");
                    }
                    break;
                case LadeskabState.DoorOpen:
                    if (e.DoorClosed)
                    {
                        _Display.displayCommands("Read rfid (press 'R')");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _Display.displayCommands("Error");
                    }
                    break;
                case LadeskabState.Locked:
                    _Display.displayCommands("Locker is occupied");
                    break;
            }
        }



        #region rfidDetected (Eventhandler)

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.isConnected())
                    {
                        _Door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now.ToLongDateString() + ": Skab låst med RFID: {0}", id);
                        }

                        _Display.displayCommands("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _Display.displayCommands("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _chargeControl.StopCharge();
                        _Door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now.ToLongDateString() + ": Skab låst op med RFID: " + id.ToString());
                        }

                        _Display.displayCommands("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _Display.displayCommands("Forkert RFID tag");
                    }

                    break;
            }
        }
        #endregion


        #region HandleRfidEvent

        private void HandleRfidEvent(object sender, RfidEventArgs e)
        {
            RfidDetected(e.Rfid_ID);
        }

        #endregion
    }
}

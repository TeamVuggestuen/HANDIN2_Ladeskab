using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public IUsbCharger _charger;
        public IDoor _Door;
        public IDisplay _Display;
        private int _oldId;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil


        public StationControl(IDoor Door, IDisplay display, IRfidReader RfidReader, IUsbCharger usbCharger)
        {

            Door.DoorEvent += HandleDoorEvent;                  //attach to door event
            _Door = Door;

            RfidReader.RfidEvent += HandleRfidEvent;            //attach to rfid event

            usbCharger.CurrentValueEvent += HandleUsbCharger;   //attach to USB event
            _charger = usbCharger;

            _Display = display;
        }


        #region HandleDoorEvent
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
                        _Display.displayCommands("Connect phone (and close door(press 'r'))");
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

        #region team David alt

        //private void HandleDoorEvent(object sender, DoorEventArgs e)
        //{
        //    switch (_state)
        //    {
        //        case LadeskabState.Available:
        //            doorOpenedEvent(e);
        //            break;
        //        case LadeskabState.DoorOpen:
        //            doorClosedEvent(e);
        //            break;
        //        case LadeskabState.Locked:
        //            _Display.DisplayLockerOccupied();
        //    }
        //}

        //private void doorOpenedEvent(DoorEventArgs e)
        //{
        //    if (!e.DoorClosed)
        //    {
        //        _Display.ConnectPhoneRequest();
        //        _state = LadeskabState.DoorOpen;
        //    }
        //    else
        //    {
        //        _Display.RemovePhoneRequest();
        //    }
        //}

        //private void doorClosedEvent(DoorEventArgs e)
        //{
        //    if (e.DoorClosed)
        //    {
        //        _Display.ReadRFIDRequest();
        //        _state = LadeskabState.Available;
        //    }
        //    else
        //    {
        //        Console.WriteLine("");
        //    }
        //}        

        #endregion

        #endregion


        private void HandleUsbCharger(object sender, CurrentEventArgs e)
        {
            // VED IKKE LIGE HVAD SKAL HENVISE TIL HER
        }


        #region rfidDetected (Eventhandler)

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _Door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _Door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
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

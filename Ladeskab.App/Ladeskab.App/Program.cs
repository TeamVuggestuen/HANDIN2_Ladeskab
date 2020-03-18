using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Core.Library.Classes;
using Ladeskab.Core.Library.Interfaces;

namespace Ladeskab
{
    class Program
    {
        static void Main(string[] args)
        {
            //_stationControl = new StationControl(_door, _display, _rfidReader, _usbChargerSimulator);
            Door _door = new Door();
            Display _display = new Display();
            UsbChargerSimulator _charger = new UsbChargerSimulator();
            RfidReader _rfidReader = new RfidReader();
            ChargeControl _chargeControl = new ChargeControl(_charger, _display);

            StationControl _control = new StationControl(_door, _display, _rfidReader, _chargeControl);

            bool finish = false;

            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        _door.OnDoorOpen();
                        break;

                    case 'C':
                        _door.OnDoorClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        _rfidReader.onRfidRead(id); // LAVER FEJL TIL DEN ER IMPLEMENTERET
                        break;

                    default:
                        break;
                }
            } while (!finish);
        }
    }
}

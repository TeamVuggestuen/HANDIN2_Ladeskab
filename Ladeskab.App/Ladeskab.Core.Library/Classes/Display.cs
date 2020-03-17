using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Display : IDisplay
    {
        public void ConnectPhoneRequest()
        {
            Console.WriteLine(" Press 'C' for 'Connect Phone'");
        }

        public void ReadRFIDRequest()
        {
            Console.WriteLine("Press 'R' for 'Read RFID'");
        }

        public void RemovePhoneRequest()
        {
            Console.WriteLine("Remove phone");
        }

        public void DisplayConnectionError()
        {
            Console.WriteLine("Connection error");
        }

        public void DisplayLockerOccupied()
        {
            Console.WriteLine("Charger locker occupied");
        }

        public void DisplayRFIDError()
        {
            Console.WriteLine("RFID error");
        }

        public void Error()
        {
            Console.WriteLine("Error");
        }
    }
}

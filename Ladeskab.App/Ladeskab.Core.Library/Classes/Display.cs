using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Ladeskab
{
    public class Display : IDisplay
    {
        private string text = "";
        public void displayCommands(string command)
        {
            text = command;
            Console.WriteLine(command);
        }

        public string Text
        {
            get { return text; }
        }
        #region altern

        //public void ConnectPhoneRequest()
        //{
        //    Console.WriteLine(" Press 'C' for 'Connect Phone'");
        //}

        //public void ReadRFIDRequest()
        //{
        //    Console.WriteLine("Press 'R' for 'Read RFID'");
        //}

        //public void RemovePhoneRequest()
        //{
        //    Console.WriteLine("Remove phone");
        //}

        //public void DisplayConnectionError()
        //{
        //    Console.WriteLine("Connection error");
        //}

        //public void DisplayLockerOccupied()
        //{
        //    Console.WriteLine("Charger locker occupied");
        //}

        //public void DisplayRFIDError()
        //{
        //    Console.WriteLine("RFID error");
        //}

        //public void Error()
        //{
        //    Console.WriteLine("Error");
        //}
        #endregion
    }
}

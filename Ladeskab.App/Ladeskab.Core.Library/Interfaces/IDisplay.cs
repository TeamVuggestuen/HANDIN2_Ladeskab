using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    interface IDisplay
    {
        // kald som StationControl skal kunne foretage på et display
        void ConnectPhoneRequest();
        void RemovePhoneRequest();
        void ReadRFIDRequest();

        void DisplayConnectionError();
        void DisplayLockerOccupied();
        void DisplayRFIDError();

        void Error();
    }
}

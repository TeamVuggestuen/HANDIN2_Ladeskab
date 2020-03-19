using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IDisplay
    {
        // kald som Controls skal kunne foretage på et display
        void displayCommands(string command);

        #region altern

        //void ConnectPhoneRequest();
        //void RemovePhoneRequest();
        //void ReadRFIDRequest();

        //void DisplayConnectionError();
        //void DisplayLockerOccupied();
        //void DisplayRFIDError();

        //void Error();        

        #endregion

    }
}

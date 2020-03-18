using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Core.Library.Interfaces
{
    interface IChargeControl
    {
        event EventHandler<ChargeDisplayEventArgs> ChargeDisplayEvent;
        void updateDisplayPower(double value);
        bool isConnected();
        void startCharge();
        void stopCharge();

        double CurrentCharge { get; set; }
    }
}

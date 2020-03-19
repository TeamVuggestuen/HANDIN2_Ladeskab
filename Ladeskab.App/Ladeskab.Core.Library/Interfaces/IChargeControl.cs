using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Core.Library.Interfaces
{
    public interface IChargeControl
    {
        bool isConnected();
        void StartCharge();
        void StopCharge();
    }
}

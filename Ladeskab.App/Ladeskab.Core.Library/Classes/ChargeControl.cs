using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Core.Library.Interfaces;

namespace Ladeskab.Core.Library.Classes
{
    class ChargeControl : IChargeControl
    {
        //member
        public IUsbCharger _charger;
        public IDisplay _chargerDisplay;

        public ChargeControl(IUsbCharger charger, IDisplay chargerDisplay)
        {
            charger.CurrentValueEvent += HandleUsbCharger;
            _charger = charger;
            _chargerDisplay = chargerDisplay;
        }

        private void HandleUsbCharger(object sender, CurrentEventArgs e)
        {
            // VED IKKE LIGE HVAD SKAL HENVISE TIL HER
        }
    }
}

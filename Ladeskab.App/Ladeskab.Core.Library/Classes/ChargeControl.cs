using System.Collections;
using Ladeskab.Core.Library.Interfaces;

namespace Ladeskab.Core.Library.Classes
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _charger;
        private IDisplay _chargerDisplay;

        private int _countToPrintOnce = 0;

        private double oldcurrent { get; set; }

        public bool Connected { get; set; }

        // beskeder
        public string Overloadmessage = "Kortslutning. Fjern telefonen.";
        public string Connectedmessage = "Telefonen er tilsluttet og lader";
        public string Chargedmessage = "Telefonen er fuldt opladet. Fjern telefonen.";


        public ChargeControl(IUsbCharger charger, IDisplay chargerDisplay)
        {
            charger.CurrentValueEvent += HandleChargerEvent;
            _charger = charger;
            _chargerDisplay = chargerDisplay;
        }

        private void HandleChargerEvent(object sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                Connected = false;
            }
            else if (e.Current > 0 && e.Current <= 5)
            {
                Connected = true;
                _countToPrintOnce++;
                if (_countToPrintOnce==1)
                {
                    _chargerDisplay.displayCommands(Chargedmessage);
                }
            }
            else if (e.Current > 5 && e.Current <= 500)
            {
                Connected = true;
                _countToPrintOnce++;
                if (_countToPrintOnce == 1)
                {
                    _chargerDisplay.displayCommands(Connectedmessage);
                }
                oldcurrent = e.Current;
            }
            else if (e.Current > 500)
            {
                Connected = true;
                _countToPrintOnce++;
                if (_countToPrintOnce == 1)
                {
                    _chargerDisplay.displayCommands(Overloadmessage);
                }
            }
        }


        public bool isConnected()
        {
            if (_charger.Connected)
            {
                return true;
            }

            return false;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }
    }
}

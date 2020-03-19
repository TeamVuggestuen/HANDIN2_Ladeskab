using Ladeskab.Core.Library.Interfaces;

namespace Ladeskab.Core.Library.Classes
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger _charger;
        public IDisplay _chargerDisplay;

        public bool Connected { get; set; }

        public string Errormessage = "Kortslutning. Fjern telefonen.";

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
                _chargerDisplay.displayCommands(e.Current.ToString());
            }
            else if (e.Current > 5 && e.Current <= 500)
            {
                Connected = true;
                _chargerDisplay.displayCommands(e.Current.ToString());
            }
            else
            {
                Connected = true;
                _chargerDisplay.displayCommands(Errormessage);
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

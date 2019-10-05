using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;
using Tankstelle.GUI;

namespace Tankstelle.Business
{
    public class GasPump
    {
        private GasPumpDisplay _display = new GasPumpDisplay();        
        private bool _isLocked = false;

        /// <summary>
        /// Legt fest ob Zapfsäule in Gebrauch ist.
        /// </summary>
        public bool IsLocked { get; set; }
        public int GasPumpNumber { get; set; }
        public decimal ToPayValue { get; set; }
        public ActiveFuelPropertys ActiveFuel { get; set; }
        public void OpenDisplay()
        {
            _display.Show();
        }
        public GasPump(int gasPumpNumber)
        {
            GasPumpNumber = gasPumpNumber;
        }
    }
}

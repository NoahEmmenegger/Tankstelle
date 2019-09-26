using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;

namespace Tankstelle.Business
{
    public class GasPump
    {
        private bool _isLocked = false;
        public int GasPumpNumber { get; set; }
        public List<Fuel> FuelList { get; set; }
        public decimal ToPayValue { get; set; }
        public ActiveFuelPropertys ActiveFuel { get; set; }
        public void OpenDisplay()
        {

        }
        public GasPump(List<Fuel> fuelList, int GasPumpNumber)
        {
            FuelList = fuelList;
        }
    }
}

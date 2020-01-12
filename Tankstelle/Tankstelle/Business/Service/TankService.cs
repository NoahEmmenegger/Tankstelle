using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business.TankService
{
    /// <summary>
    /// Erhalte weitere Informationen zu einem Tank
    /// </summary>
    class TankService
    {
        /// <summary>
        /// Schaue, ob noch genug Liter im Tank ist
        /// </summary>
        /// <param name="tank"></param>
        /// <returns></returns>
        public static bool HasEnoughInTank(Tank tank)
        {
            return tank.VolumeLiter >= tank.MinAmount;
        }

        /// <summary>
        /// Vergleiche mit dem letzt Jährigen Monat
        /// </summary>
        /// <param name="tank"></param>
        /// <returns></returns>
        public static bool AdjustTankMinimum(Tank tank)
        {
            return tank.AvailibleLiter - GetOutgoingLiter(tank, DateTime.Now.AddYears(-1)) <= tank.MinAmount;
        }

        /// <summary>
        /// Erhalte die verkauften Liter eines Datums
        /// </summary>
        /// <param name="tank"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static float GetOutgoingLiter(Tank tank, DateTime date)
        {
            float liters = 0;
            foreach (Receipt receipt in ReceiptService.GetReceipts().Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year))
            {
                if (tank.FuelName == receipt.RelatedFuel.Name)
                {
                    liters += receipt.RelatedLiter;
                }
            }
            return liters;
        }
    }
}

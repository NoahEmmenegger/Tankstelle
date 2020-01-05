using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business.TankService
{
    class TankService
    {
        public static bool HasEnoughInTank(Tank tank)
        {
            return tank.VolumeLiter >= tank.MinAmount;
        }

        public static bool AdjustTankMinimum(Tank tank)
        {
            return GetOutgoingLiter(tank, DateTime.Now.AddYears(-1)) <= tank.MinAmount;
        }

        public static int GetOutgoingLiter(Tank tank, DateTime date)
        {
            int liters = 0;
            foreach (Receipt receipt in ReceiptService.GetReceipts().Where(x => x.Date.Month == date.Month))
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

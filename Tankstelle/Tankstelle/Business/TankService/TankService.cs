using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business.TankService
{
    class TankService
    {
        public bool HasEnoughInTank(Tank tank)
        {
            return tank.VolumeLiter >= tank.MinAmount;
        }

        public bool AdjustTankMinimum(Tank tank)
        {
            return true;
        }
    }
}

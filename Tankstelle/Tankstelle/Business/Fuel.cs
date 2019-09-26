using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Fuel
    {
        public decimal PricePerLiter { get; set; }
        public List<Tank> TankList { get; set; }
        public string Name { get; set; }

        public Fuel()
        {

        }

        public Fuel(List<Tank> tankList)
        {
            TankList = tankList;
        }
    }
}

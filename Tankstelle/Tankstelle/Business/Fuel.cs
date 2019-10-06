using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Fuel
    {
        /// <summary>
        /// Preis wie Teuer ein Liter vom Treibstoff ist.
        /// </summary>
        public decimal PricePerLiter { get; set; }
        /// <summary>
        /// Liste von den Tanks, welche diese Treibstoffsorte beinhalten
        /// </summary>
        public List<Tank> TankList { get; set; }
        /// <summary>
        /// Name von der Treibstoffsorte
        /// </summary>
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

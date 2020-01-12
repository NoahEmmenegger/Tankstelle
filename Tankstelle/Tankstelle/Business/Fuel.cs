using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    /// <summary>
    /// Treibstoff
    /// </summary>
    public class Fuel : IFuel
    {
        /// <summary>
        /// Preis wie Teuer ein Liter vom Treibstoff ist. Angabe in Rappen.
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
    }
}

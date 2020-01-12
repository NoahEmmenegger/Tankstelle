using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    /// <summary>
    /// Interface für Treibstoffsorte
    /// </summary>
    public interface IFuel
    {
        /// <summary>
        /// Preis wie Teuer ein Liter vom Treibstoff ist. Angabe in Rappen.
        /// </summary>
        decimal PricePerLiter { get; set; }
        /// <summary>
        /// Liste von den Tanks, welche diese Treibstoffsorte beinhalten
        /// </summary>
        List<Tank> TankList { get; set; }
        /// <summary>
        /// Name von der Treibstoffsorte
        /// </summary>
        string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    /// <summary>
    /// Interface für Treibstoff Statistik
    /// </summary>
    public interface IFuelStatistic
    {
        /// <summary>
        /// Der Treibstoff, welcher Angezeigt wird
        /// </summary>
        Fuel Fuel { get; set; }
        /// <summary>
        /// Die verkauften Liters des Treibstoffes
        /// </summary>
        float SoldLiters { get; set; }
        /// <summary>
        /// Der Umsatz des Treibstoffes
        /// </summary>
        decimal Earnings { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business.Statistics
{
    /// <summary>
    /// Statistikdaten für Treibstoff
    /// </summary>
    public class FuelStatistic : IFuelStatistic
    {
        /// <summary>
        /// Der Treibstoff, welcher Angezeigt wird
        /// </summary>
        public Fuel Fuel { get; set; }
        /// <summary>
        /// Die verkauften Liters des Treibstoffes
        /// </summary>
        public float SoldLiters { get; set; }
        /// <summary>
        /// Der Umsatz des Treibstoffes
        /// </summary>
        public decimal Earnings { get; set; }
    }
}

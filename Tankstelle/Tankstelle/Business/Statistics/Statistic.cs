using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business.Statistics
{
    /// <summary>
    /// Statistik, welche ausgewählt wurde
    /// </summary>
    public class Statistic : IStatistic
    {
        /// <summary>
        /// Der ausgewählte Monat
        /// </summary>
        public int Monat { get; set; }
        /// <summary>
        /// Die Einnahmen des ausgewählten Monats
        /// </summary>
        public decimal Earnings { get; set; }
        /// <summary>
        /// Die Ausgaben des ausgewählten Monats
        /// </summary>
        public decimal Outgoings { get; set; }
        /// <summary>
        /// Der Umsatz des ausgewählten Monats
        /// </summary>
        public decimal MetabolicRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Interfaces
{
    /// <summary>
    /// Interface für Statistik
    /// </summary>
    public interface IStatistic
    {
        /// <summary>
        /// Der ausgewählte Monat
        /// </summary>
        int Monat { get; set; }
        /// <summary>
        /// Die Einnahmen des ausgewählten Monats
        /// </summary>
        decimal Earnings { get; set; }
        /// <summary>
        /// Die Ausgaben des ausgewählten Monats
        /// </summary>
        decimal Outgoings { get; set; }
        /// <summary>
        /// Der Umsatz des ausgewählten Monats
        /// </summary>
        decimal MetabolicRate { get; set; }
    }
}

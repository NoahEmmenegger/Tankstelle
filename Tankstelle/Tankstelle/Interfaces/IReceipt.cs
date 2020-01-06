using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    public interface IReceipt
    {
        /// <summary>
        /// Die eindeutige Id der Rechnung
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Das Datum und die Uhrzeit, wann die Rechnung erstellt wurde
        /// </summary>
        DateTime Date { get; set; }
        /// <summary>
        /// Die Treibstoffsorte, welche beim Tanken ausgesucht wurde.
        /// </summary>
        Fuel RelatedFuel { get; set; }
        /// <summary>
        /// Die Anzahl Liter, welche getankt wurden
        /// </summary>
        float RelatedLiter { get; set; }
        /// <summary>
        /// Der Gesamtbetrag von der Rechnung. Wieviel der Kunde bezahlen musste.
        /// </summary>
        decimal Sum { get; set; }
    }
}

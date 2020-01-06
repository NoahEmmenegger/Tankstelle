using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    public class Receipt : IReceipt
    {
        /// <summary>
        /// Die eindeutige Id der Rechnung
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Das Datum und die Uhrzeit, wann die Rechnung erstellt wurde
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Die Treibstoffsorte, welche beim Tanken ausgesucht wurde.
        /// </summary>
        public Fuel RelatedFuel { get; set; }
        /// <summary>
        /// Die Anzahl Liter, welche getankt wurden
        /// </summary>
        public float RelatedLiter { get; set; }
        /// <summary>
        /// Der Gesamtbetrag von der Rechnung. Wieviel der Kunde bezahlen musste.
        /// </summary>
        public decimal Sum { get; set; }
    }
}

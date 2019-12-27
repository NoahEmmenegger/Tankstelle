using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;
using Tankstelle.Enums;

namespace Tankstelle.Interfaces
{
    public interface IGasPump
    {
        /// <summary>
        /// Liste mit allen Zapfhähnen, welche es bei dieser Zapfsäule gibt.
        /// </summary>
        List<Tap> TapList { get; set; }
        /// <summary>
        /// Der Zapfhahn, welcher momentan im Gebrauch ist
        /// </summary>
        Tap ActiveTap { get; set; }
        /// <summary>
        /// Zeigt auf in welchem Status sich dieser Zapfhan gerade befindet, ob z.B. getankt wird.
        /// </summary>
        Statuse Status { get; set; }
        /// <summary>
        /// Nummer von der Zapfsäule
        /// </summary>
        int GasPumpNumber { get; set; }
        /// <summary>
        /// Wert welcher bei dieser Zapfäule bezahlt werden muss.
        /// </summary>
        decimal ToPayValue { get; set; }

        void FinishRefuel();
        void StartRefuel();

        /// <summary>
        /// Anzahl der bereits getankten Liter an dieser Zapfsäule vom akktuellen Kunden
        /// </summary>
        double Liter { get; set; }

        void StopRefuel();
    }
}

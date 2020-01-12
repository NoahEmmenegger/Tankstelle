using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    /// <summary>
    /// Interface für Tankstelle
    /// </summary>
    public interface IGasStation
    {
        /// <summary>
        /// Liste mit allen Zapfsäulen
        /// </summary>
        List<GasPump> GasPumpList { get; set; }
        // <summary>
        /// Die Kasse von der Tankstelle
        /// </summary>
        CashRegister chashRegister { get; set; }
        /// <summary>
        /// Holt die Informationen über die GasPumps, welche im Config stehen und erzeugt anhand dieser Informationen GasPumps.
        /// </summary>
        void GetGasPumps();
        /// <summary>
        /// Holt die Informationen über die Treibstoffarten, welche im Config stehen.
        /// </summary>
        void GetFuels();
        /// <summary>
        /// Holt alle Tänke, welche im Config stehen.
        /// </summary>
        void GetTanks();
        /// <summary>
        /// Holt alle Quittungen, welche im Config stehen.
        /// </summary>
        void GetReceipt();
    }
}

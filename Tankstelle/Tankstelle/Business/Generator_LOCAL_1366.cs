using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Data;

namespace Tankstelle.Business
{
    /// <summary>
    /// Ist für die Erstellung der Tankstelle zuständig
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Erstellt die Tankstelle, damit Sie einstatzfähig ist.
        /// </summary>
        public static GasStation Generate()
        {
            GasStation gasStation = GasStation.GetInstance();
            IConfigurationManager configurationManager = ConfigurationManager.CreateInstance();
            gasStation.SetConfigurationManager(configurationManager);
            gasStation.GetFuels();
            gasStation.GetTanks();
            gasStation.GetReceipt();
            gasStation.GetGasPumps();
            return gasStation;
        }
    }
}

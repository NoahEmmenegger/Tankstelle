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
        public static void Generate()
        {
            IConfigurationManager configurationManager = ConfigurationManager.CreateInstance();
            GasStation.GetInstance().SetConfigurationManager(configurationManager);
            GasStation.GetInstance().GetFuels();
            GasStation.GetInstance().GetTanks();
            GasStation.GetInstance().GetGasPumps();
        }
    }
}

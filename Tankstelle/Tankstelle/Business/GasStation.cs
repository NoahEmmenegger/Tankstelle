using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle;
using Tankstelle.Data;

namespace Tankstelle.Business
{
    public class GasStation
    {
        private ConfigurationManager _configManager = ConfigurationManager.CreateInstance();
        public List<GasPump> GasPumpList { get; set; }
        public List<Fuel> FuelList { get; set; }

        /// <summary>
        /// Holt die Werte, welche im Config stehen und setzt sie
        /// </summary>
        public void GetGasPumps()
        {
            _configManager.GetNumberOfGasPump();
        }

        public void SetGasPumps()
        {

        }

        public void GetFuels()
        {
            _configManager.GetFuels();
        }

        public void SetFuels()
        {

        }
    }
}

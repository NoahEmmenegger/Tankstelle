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
        /// Holt die Informationen über die GasPumps, welche im Config stehen und erzeugt anhand dieser Informationen GasPumps.
        /// </summary>
        public void GetGasPumps()
        {
            GetFuels();
            for (int i = 0; i < _configManager.GetNumberOfGasPump(); i++)
            {
                GasPumpList.Add(new GasPump(FuelList, i + 1));
            }
        }

        public void SetGasPumps()
        {

        }

        /// <summary>
        /// Holt die Informationen über die Treibstoffarten, welche im Config stehen.
        /// </summary>
        public void GetFuels()
        {
            foreach (Fuel oneFuel in _configManager.GetFuels())
            {
               oneFuel.TankList = _configManager.GetTanks().Where(t => t._name == oneFuel.Name).ToList();
               FuelList.Add(oneFuel);
            }
        }

        public void SetFuels()
        {

        }

    }
}

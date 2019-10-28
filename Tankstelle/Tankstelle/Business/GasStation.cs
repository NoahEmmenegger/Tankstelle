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
        private static GasStation instance = null;
        private static List<Fuel> fuelList = new List<Fuel>();
        private static List<GasPump> gasPumpList = new List<GasPump>();

        /// <summary>
        /// Objekt um aus der Config Datei zu lesen und in die Config Datei zu schreiben.
        /// </summary>
        private ConfigurationManager _configManager = ConfigurationManager.CreateInstance();
        /// <summary>
        /// Liste mit allen Zapfsäulen
        /// </summary>
        public List<GasPump> GasPumpList { get => gasPumpList; set => gasPumpList = value; }
        /// <summary>
        /// Liste mit allen Treibstoffsorten
        /// </summary>
        public List<Fuel> FuelList { get => fuelList; set => fuelList = value; }

        private GasStation()
        {
            //Do Nothing
        }

        public static GasStation GetInstance()
        {
            if (instance == null)
                instance = new GasStation();
            return instance;
        }


        /// <summary>
        /// Holt die Informationen über die GasPumps, welche im Config stehen und erzeugt anhand dieser Informationen GasPumps.
        /// </summary>
        public void GetGasPumps()
        {
            GasPumpList.Clear();
            for (int i = 0; i < _configManager.GetNumberOfGasPump(); i++)
            {
                GasPumpList.Add(new GasPump(i + 1));
            }
        }
        /// <summary>
        /// Setzt die Zapfsäulen im Config
        /// </summary>
        public void SetGasPumps(int numberOfGasPump)
        {
            _configManager.SetNumberOfGasPump(numberOfGasPump);
            _configManager.SaveChanges();
        }

        /// <summary>
        /// Holt die Informationen über die Treibstoffarten, welche im Config stehen.
        /// </summary>
        public void GetFuels()
        {
            FuelList.Clear();
            foreach (Fuel oneFuel in _configManager.GetFuels())
            {
               oneFuel.TankList = _configManager.GetTanks().Where(t => t.Name == oneFuel.Name).ToList();
               FuelList.Add(oneFuel);
            }
        }

        /// <summary>
        /// Setzt die Treibstoffarten im Config
        /// </summary>
        public static void SetFuels()
        {

        }

    }
}

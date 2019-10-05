﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle;
using Tankstelle.Data;

namespace Tankstelle.Business
{
    public static class GasStation
    {
        private static List<Fuel> fuelList = new List<Fuel>();
        private static List<GasPump> gasPumpList = new List<GasPump>();

        private static ConfigurationManager _configManager = ConfigurationManager.CreateInstance();
        public static List<GasPump> GasPumpList { get => gasPumpList; set => gasPumpList = value; }
        public static List<Fuel> FuelList { get => fuelList; set => fuelList = value; }

        /// <summary>
        /// Holt die Informationen über die GasPumps, welche im Config stehen und erzeugt anhand dieser Informationen GasPumps.
        /// </summary>
        public static void GetGasPumps()
        {
            for (int i = 0; i < _configManager.GetNumberOfGasPump(); i++)
            {
                GasPumpList.Add(new GasPump(i + 1));
            }
        }

        public static void SetGasPumps()
        {

        }

        /// <summary>
        /// Holt die Informationen über die Treibstoffarten, welche im Config stehen.
        /// </summary>
        public static void GetFuels()
        {
            foreach (Fuel oneFuel in _configManager.GetFuels())
            {
               oneFuel.TankList = _configManager.GetTanks().Where(t => t.Name == oneFuel.Name).ToList();
               FuelList.Add(oneFuel);
            }
        }

        public static void SetFuels()
        {

        }

    }
}

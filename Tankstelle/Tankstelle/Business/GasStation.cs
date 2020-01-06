﻿using System;
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
        private List<Fuel> fuelList = new List<Fuel>();
        private List<CashRegister> cashRegisterList = new List<CashRegister>();
        private List<Tank> tankList = new List<Tank>();

        /// <summary>
        /// Objekt um aus der Config Datei zu lesen und in die Config Datei zu schreiben.
        /// </summary>
        private IConfigurationManager _configManager;
        /// <summary>
        /// Liste mit allen Zapfsäulen
        /// </summary>
        public List<GasPump> GasPumpList { get; set; } = new List<GasPump>();
        /// <summary>
        /// Liste mit allen Treibstoffsorten
        /// </summary>
        public List<Fuel> FuelList { get => fuelList; set => fuelList = value; }
        /// <summary>
        /// Liste mit allen Treibstoffsorten
        /// </summary>
        public List<Tank> TankList { get => tankList; set => tankList = value; }

        /// <summary>
        /// Liste mit allen Kassen von der Tankstelle
        /// </summary>
        public List<CashRegister> CashRegisterList
        {
            get
            {
                return cashRegisterList;
            }
            set
            {
                cashRegisterList = value;
            }
        }

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
            for (int i = 0; i < _configManager.GetGasPumps().Count(); i++)
            {
                GasPumpList.Add(new GasPump(i + 1));
            }
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
        /// Holt alle Tänke, welche im Config stehen.
        /// </summary>
        public void GetTanks()
        {
            TankList.Clear();
            TankList = _configManager.GetTanks();
        }

        /// <summary>
        /// Setzt die Treibstoffarten im Config
        /// </summary>
        public void AddFuels(Fuel fuel)
        {
            _configManager.AddFuel(fuel);
            _configManager.SaveChanges();
        }

        /// <summary>
        /// Setzt den Configuration Manager
        /// </summary>
        /// <param name="configurationManager">Mitgegebenes Configuration Manager Objekt</param>
        public void SetConfigurationManager(IConfigurationManager configurationManager)
        {
            _configManager = configurationManager;
        }
    }
}

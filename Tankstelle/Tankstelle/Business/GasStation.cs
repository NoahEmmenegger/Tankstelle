using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle;
using Tankstelle.Data;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    /// <summary>
    /// Ist die zentralle Businessklasse von hier aus, hat man überallhin zugriff.
    /// </summary>
    public class GasStation : IGasStation
    {
        #region private Felder
        /// <summary>
        /// Das Singelton Objekt von der Klasse GasStation
        /// </summary>
        private static GasStation instance = null;
        /// <summary>
        /// Objekt um aus der Config Datei zu lesen und in die Config Datei zu schreiben.
        /// </summary>
        private IConfigurationManager _configManager;
        #endregion

        #region public Propertys
        /// <summary>
        /// Liste mit allen Zapfsäulen
        /// </summary>
        public List<GasPump> GasPumpList { get; set; } = new List<GasPump>();
        /// <summary>
        /// Liste mit allen Treibstoffsorten
        /// </summary>
        public List<Fuel> FuelList { get; set; } = new List<Fuel>();
        /// <summary>
        /// Liste mit allen Tanksorten
        /// </summary>
        public List<Tank> TankList { get; set; } = new List<Tank>();
        /// <summary>
        /// Liste mit allen Kassen von der Tankstelle
        /// </summary>
        public List<CashRegister> CashRegisterList { get; set; }
        #endregion

        #region Konstruktor
        private GasStation()
        {
            //Do Nothing
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Erstellt eine neue Instanz, sofern noch keine vorhanden ist und ansonsten wird die vorhandene Instanz zurückgegeben.
        /// </summary>
        /// <returns></returns>
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

        public void AddReceipt(Receipt receipt)
        {
            _configManager.AddReceipt(receipt);
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
        #endregion
    }
}

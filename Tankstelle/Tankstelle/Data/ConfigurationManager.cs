using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using Tankstelle.Business;
using Tankstelle.Business.TankService;
using Tankstelle.Enums; 

namespace Tankstelle.Data
{
    /// <summary>
    /// Erhalte die Daten als Objekt, welche im config stehen und speichere Änderungen ab
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        #region private Felder
        /// <summary>
        /// Liste von allen Zapfsäulen
        /// </summary>
        private List<GasPump> gasPumps = new List<GasPump>();
        /// <summary>
        /// Liste mit allen Treibstoffsorten
        /// </summary>
        private List<Fuel> fuels = new List<Fuel>();
        /// <summary>
        /// Liste von allen Tanks
        /// </summary>
        private List<Tank> tanks = new List<Tank>();
        /// <summary>
        /// Liste von Quitungen
        /// </summary>
        private List<Receipt> receipts = new List<Receipt>();
        /// <summary>
        /// Liste von Geld
        /// </summary>
        private List<Coin> coins = new List<Coin>();
        /// <summary>
        /// Instanz von ConfigurationManager
        /// </summary>
        private static ConfigurationManager uniqueInstance = null;
        #endregion

        #region Konstruktor
        private ConfigurationManager()
        {

        }
        #endregion

        #region Methoden
        /// <summary>
        /// Erstellt oder nimmt eine Instanz und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static ConfigurationManager CreateInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new ConfigurationManager();
                uniqueInstance.GetDataAsJson();
            }
            return uniqueInstance;
        }

        /// <summary>
        /// Neue Zapfsäulen hinzufügen
        /// </summary>
        /// <param name="gasPump"></param>
        public void AddGasPump(GasPump gasPump)
        {
            gasPumps.Add(gasPump);
        }

        /// <summary>
        /// Alle Zapfsäulen erhalten
        /// </summary>
        /// <returns></returns>
        public List<GasPump> GetGasPumps()
        {
            return gasPumps;
        }

        /// <summary>
        /// Neue Treibstoffsorten hinzufügenss
        /// </summary>
        /// <param name="fuel"></param>
        public void AddFuel(Fuel fuel)
        {
            fuels.Add(fuel);
        }

        /// <summary>
        /// Erhalte alle Treibstoffsorten
        /// </summary>
        /// <returns></returns>
        public List<Fuel> GetFuels()
        {
            return fuels;
        }

        /// <summary>
        /// Erhalte Treibstoffsorte anhand dem Namen
        /// </summary>
        /// <param name="fuelName"></param>
        /// <returns></returns>
        private Fuel GetFuelByName(string fuelName)
        {
            foreach (Fuel fuel in fuels)
            {
                if (fuel.Name == fuelName)
                {
                    return fuel;
                }
            }
            MessageService.AddErrorMessage("Bitte überprüfen Sie ihr receiptConfig.json", "Ungültige Rechnung mit dem fuelName " + fuelName + " wurde hinzugefügt");
            return null;
        }

        /// <summary>
        /// Neuer Tank hinzufügen
        /// </summary>
        /// <param name="tank"></param>
        public void AddTank(Tank tank)
        {
            tanks.Add(tank);
        }

        /// <summary>
        /// Erhalte alle Tanks
        /// </summary>
        /// <returns></returns>
        public List<Tank> GetTanks()
        {
            return tanks;
        }

        /// <summary>
        /// Erhalte ein Tank anhand der Nummer
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private Tank GetTankByNumber(int number)
        {
            foreach (Tank tank in tanks)
            {
                if (tank.Number == number)
                {
                    return tank;
                }
            }
            MessageService.AddErrorMessage("Bitte überprüfen Sie ihr fuelConfig.json", "Ungültiger Tank mit der Id " + number + " wurde hinzugefügt");
            return null;
        }

        /// <summary>
        /// Erhalte Quittungen
        /// </summary>
        /// <returns></returns>
        public List<Receipt> GetReceipts()
        {
            return receipts;
        }

        /// <summary>
        /// Neue Quittung hinzufügen
        /// </summary>
        /// <param name="receipt"></param>
        public void AddReceipt(Receipt receipt)
        {
            receipts.Add(receipt);
        }

        /// <summary>
        /// Erhalte alle Münzen
        /// </summary>
        /// <returns></returns>
        public List<Coin> GetCoins()
        {
            return coins;
        }

        /// <summary>
        /// Neue Münze hinzufügen
        /// </summary>
        /// <param name="coin"></param>
        public void AddCoin(Coin coin)
        {
            coins.Add(coin);
        }

        #endregion

        #region GetData

        /// <summary>
        /// Scannt alle json und füllt die Daten in Objekte ab
        /// </summary>
        private void GetDataAsJson()
        {
            try
            {
                //GASPUMP
                using (StreamReader sr = new StreamReader(@"..\..\Data\config\gasPumpConfig.json"))
                {
                    string fileString = sr.ReadToEnd();
                    JToken gasPumpJson = JObject.Parse(fileString)["gasPumps"];

                    foreach (JToken gasPumpToken in gasPumpJson)
                    {
                        GasPump gasPump = new GasPump(Convert.ToInt32(gasPumpToken["GasPumpNumber"]));
                        gasPump.Status = (Statuse)Convert.ToInt32(gasPumpToken["Status"]);
                        gasPumps.Add(gasPump);
                    }
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("GasPump kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr gasPumpConfig.json");
            }

            try
            {
                //TANK
                using (StreamReader sr = new StreamReader(@"..\..\Data\config\tankConfig.json"))
                {
                    string fileString = sr.ReadToEnd();
                    JToken tankJson = JObject.Parse(fileString)["tanks"];

                    foreach (JToken tankToken in tankJson)
                    {
                        Tank tank = new Tank();
                        tank.Number = Convert.ToInt32(tankToken["Number"]);
                        tank.Name = tankToken["Name"].ToString();
                        tank.FuelName = tankToken["FuelName"].ToString();
                        tank.AvailibleLiter = float.Parse(tankToken["AvailibleLiter"].ToString());
                        tank.VolumeLiter = float.Parse(tankToken["VolumeLiter"].ToString());
                        tank.MinAmount = float.Parse(tankToken["MinAmount"].ToString());
                        tanks.Add(tank);
                    }
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("TANK kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr tankConfig.json");
            }

            try
            {
                //FUEL
                using (StreamReader sr = new StreamReader(@"..\..\Data\config\fuelConfig.json"))
                {
                    string fileString = sr.ReadToEnd();
                    JToken fuelJson = JObject.Parse(fileString)["fuels"];

                    foreach (JToken fuelToken in fuelJson)
                    {
                        Fuel fuel = new Fuel();
                        fuel.Name = fuelToken["Name"].ToString();
                        fuel.PricePerLiter = Convert.ToDecimal(fuelToken["PricePerLiter"]);
                        fuel.TankList = new List<Tank>();
                        foreach (JToken tankToken in fuelToken["TankList"])
                        {
                            Tank tank = GetTankByNumber(Convert.ToInt32(tankToken));
                            fuel.TankList.Add(tank);
                        }
                        fuels.Add(fuel);
                    }
                }
            }
            catch (Exception)
            {

                MessageService.AddErrorMessage("Fuel kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr fuelConfig.json");
            }

            try
            {
                //RECEIPT
                using (StreamReader sr = new StreamReader(@"..\..\Data\config\receiptConfig.json"))
                {
                    string fileString = sr.ReadToEnd();
                    JToken receiptJson = JObject.Parse(fileString)["receipts"];

                    foreach (JToken receiptToken in receiptJson)
                    {
                        Receipt receipt = new Receipt();
                        receipt.Id = Convert.ToInt32(receiptToken["Id"]);
                        receipt.Date = DateTime.Parse(receiptToken["Date"].ToString());
                        receipt.RelatedFuel = GetFuelByName(receiptToken["RelatedFuel"].ToString());
                        receipt.RelatedLiter = float.Parse(receiptToken["RelatedLiter"].ToString());
                        receipt.Sum = Convert.ToDecimal(receiptToken["Sum"].ToString());
                        receipts.Add(receipt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageService.AddErrorMessage("Receipt kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr receiptConfig.json");
            }

            try
            {
                //Coin
                using (StreamReader sr = new StreamReader(@"..\..\Data\config\coinConfig.json"))
                {
                    string fileString = sr.ReadToEnd();
                    JToken coinJson = JObject.Parse(fileString)["coins"];

                    foreach (JToken coinToken in coinJson)
                    {
                        Coin coin = new Coin(Convert.ToInt32(coinToken));
                        coins.Add(coin);
                    }
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("Coin kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr coinConfig.json");
            }
        }
        #endregion

        #region SaveChanges
        /// <summary>
        /// speichert alle Zapfsäulen welche in diesem Objekt existieren
        /// </summary>
        public void SaveGasPumpChanges()
        {
            try
            {
                //GASPUMP
                using (StreamWriter sr = new StreamWriter(@"..\..\Data\config\gasPumpConfig.json"))
                {
                    dynamic baseJson = new JObject();

                    JArray gasPumpJsonArray = new JArray();
                    foreach (GasPump gasPump in gasPumps)
                    {
                        dynamic gasPumpJson = new JObject();
                        gasPumpJson.GasPumpNumber = gasPump.GasPumpNumber;
                        gasPumpJson.Status = gasPump.Status;
                        gasPumpJsonArray.Add(gasPumpJson);

                    }
                    baseJson.gasPumps = gasPumpJsonArray;

                    sr.Write(baseJson.ToString());
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("GASPUMP kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr gasPumpConfig.json");
            }
        }

        /// <summary>
        /// speichert alle Tanks welche in diesem Objekt existieren
        /// </summary>
        public void SaveTankChanges()
        {
            try
            {
                //TANK
                using (StreamWriter sr = new StreamWriter(@"..\..\Data\config\tankConfig.json"))
                {
                    dynamic baseJson = new JObject();

                    JArray tankJsonArray = new JArray();
                    foreach (Tank tank in tanks)
                    {
                        dynamic tankJson = new JObject();
                        tankJson.Number = tank.Number;
                        tankJson.Name = tank.Name;
                        tankJson.FuelName = tank.FuelName;
                        tankJson.AvailibleLiter = tank.AvailibleLiter.ToString();
                        tankJson.VolumeLiter = tank.VolumeLiter.ToString();
                        tankJson.MinAmount = tank.MinAmount.ToString();
                        tankJsonArray.Add(tankJson);

                    }
                    baseJson.tanks = tankJsonArray;

                    sr.Write(baseJson.ToString());
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("Tank kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr tankConfig.json");
            }
        }

        /// <summary>
        /// speichert alle Treibstoffsorten welche in diesem Objekt existieren
        /// </summary>
        public void SaveFuelChanges()
        {
            try
            {
                //FUEL
                using (StreamWriter sr = new StreamWriter(@"..\..\Data\config\fuelConfig.json"))
                {
                    dynamic baseJson = new JObject();

                    JArray fuelJsonArray = new JArray();
                    foreach (Fuel fuel in fuels)
                    {
                        dynamic fuelJson = new JObject();
                        fuelJson.Name = fuel.Name;
                        fuelJson.PricePerLiter = fuel.PricePerLiter.ToString();
                        JArray tankJsonArray = new JArray();
                        foreach (Tank tank in fuel.TankList)
                        {
                            dynamic tankJson = new JObject();
                            tankJson.Number = tank.Number;
                            tankJsonArray.Add(tankJson);
                        }
                        fuelJson.TankList = tankJsonArray;
                        fuelJsonArray.Add(fuelJson);

                    }
                    baseJson.fuels = fuelJsonArray;

                    sr.Write(baseJson.ToString());
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("Fuel kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr fuelConfig.json");
            }
        }

        /// <summary>
        /// speichert alle Quittungen welche in diesem Objekt existieren
        /// </summary>
        public void SaveReceiptChanges()
        {
            try
            {
                //RECEIPT
                using (StreamWriter sr = new StreamWriter(@"..\..\Data\config\receiptConfig.json"))
                {
                    dynamic baseJson = new JObject();

                    JArray receiptJsonArray = new JArray();
                    foreach (Receipt receipt in receipts)
                    {
                        dynamic receiptJson = new JObject();
                        receiptJson.Id = receipt.Id;
                        receiptJson.Date = receipt.Date.ToString("yyyy'/'MM'/'dd HH:mm:ss");
                        receiptJson.RelatedFuel = receipt.RelatedFuel.Name;
                        receiptJson.RelatedLiter = receipt.RelatedLiter;
                        receiptJson.Sum = receipt.Sum;
                        receiptJsonArray.Add(receiptJson);

                    }
                    baseJson.receipts = receiptJsonArray;

                    sr.Write(baseJson.ToString());
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("Receipt kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr receiptConfig.json");
            }
        }

        /// <summary>
        /// speichert alle Münzen welche in diesem Objekt existieren
        /// </summary>
        public void SaveCoinChanges()
        {
            try
            {
                //Coin
                using (StreamWriter sr = new StreamWriter(@"..\..\Data\config\coinConfig.json"))
                {
                    dynamic baseJson = new JObject();

                    JArray coinJsonArray = new JArray();
                    foreach (Coin coin in coins)
                    {
                        coinJsonArray.Add(coin.GetValue());

                    }
                    baseJson.coins = coinJsonArray;

                    sr.Write(baseJson.ToString());
                }
            }
            catch (Exception)
            {
                MessageService.AddErrorMessage("Coin kann nicht ausgelesen werden", "Bitte überprüfe Sie ihr coinConfig.json");
            }
        }

        /// <summary>
        /// Schreibt alle Änderungen in ein json
        /// </summary>
        public void SaveAllChanges()
        {
            //GASPUMP
            SaveGasPumpChanges();

            //TANK
            SaveTankChanges();

            //FUEL
            SaveFuelChanges();

            //RECEIPT
            SaveReceiptChanges();

            //Coin
            SaveCoinChanges();
        }
        #endregion
    }
}

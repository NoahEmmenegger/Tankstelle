using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Tankstelle.Business;
using Tankstelle.Enums; 

namespace Tankstelle.Data
{
    public class ConfigurationManager : IConfigurationManager
    {
        #region private Felder
        private string filePath = @"..\..\Data\config.json";
        private List<GasPump> gasPumps = new List<GasPump>();
        private List<Fuel> fuels = new List<Fuel>();
        private List<Tank> tanks = new List<Tank>();
        private List<Receipt> receipts = new List<Receipt>();
        private List<Coin> coins = new List<Coin>();
        private static ConfigurationManager uniqueInstance = null;
        #endregion

        #region Konstruktor
        private ConfigurationManager()
        {

        }
        #endregion

        #region Methoden
        public static ConfigurationManager CreateInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new ConfigurationManager();
                uniqueInstance.GetDataAsJson();
            }
            return uniqueInstance;
        }
        public void AddGasPump(GasPump gasPump)
        {
            gasPumps.Add(gasPump);
        }

        public List<GasPump> GetGasPumps()
        {
            return gasPumps;
        }

        public void AddFuel(Fuel fuel)
        {
            fuels.Add(fuel);
        }

        public List<Fuel> GetFuels()
        {
            return fuels;
        }

        public void AddTank(Tank tank)
        {
            tanks.Add(tank);
        }

        public List<Tank> GetTanks()
        {
            return tanks;
        }

        public Tank GetTankByNumber(int number)
        {
            foreach (Tank tank in tanks)
            {
                if (tank.Number == number)
                {
                    return tank;
                }
            }
            return null;
        }

        public Fuel GetFuelByName(string fuelName)
        {
            foreach (Fuel fuel in fuels)
            {
                if (fuel.Name == fuelName)
                {
                    return fuel;
                }
            }
            return null;
        }

        public List<Receipt> GetReceipts()
        {
            return receipts;
        }

        public void AddReceipt(Receipt receipt)
        {
            receipts.Add(receipt);
        }

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
            catch (Exception ex)
            {
                throw ex;
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

                throw;
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
                            Tank tank = GetTankByNumber(Convert.ToInt32(tankToken["Number"]));
                            fuel.TankList.Add(tank);
                        }
                        fuels.Add(fuel);
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                        receipt.RelatedLiter = Convert.ToInt32(receiptToken["RelatedLiter"].ToString());
                        receipt.Sum = Convert.ToInt32(receiptToken["Sum"].ToString());
                        receipts.Add(receipt);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveChanges()
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
        #endregion
    }
}

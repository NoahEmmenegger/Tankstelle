using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Data
{
    public class ConfigurationManager
    {
        private string filePath = @"..\..\Data\config.json";
        private int numberOfGasPump;
        private List<Fuel> fuels = new List<Fuel>();
        private List<Tank> tanks = new List<Tank>();
        private static ConfigurationManager uniqueInstance = null;

        public static ConfigurationManager CreateInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new ConfigurationManager();
                uniqueInstance.GetDataAsJson();
            }
            return uniqueInstance;
        }

        private ConfigurationManager()
        {

        }

        public void SetNumberOfGasPump(int NumberOfGasPump)
        {
            numberOfGasPump = NumberOfGasPump;
        }

        public int GetNumberOfGasPump()
        {
            return numberOfGasPump;
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

        public void GetDataAsJson()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string rawJson = sr.ReadToEnd();

                JObject jObject = JObject.Parse(rawJson);
                JToken data = jObject["Data"];

                numberOfGasPump = (int)data["numberOfGasPump"];

                var tanksData = data["tanks"];
                foreach (JToken tankToken in tanksData)
                {
                    Tank tankObject = new Tank();
                    tankObject._name = tankToken["name"].ToString();
                    tankObject._availibleLiter = float.Parse(tankToken["availibleLiter"].ToString());
                    tankObject._minAmount = float.Parse(tankToken["minAmount"].ToString());
                    tanks.Add(tankObject);
                }

                var fuelsData = data["fuels"];
                foreach (JToken fuel in fuelsData)
                {
                    Fuel fuelObject = new Fuel();
                    fuelObject.Name = fuel["name"].ToString();
                    fuelObject.PricePerLiter = Convert.ToDecimal(fuel["pricePerLiter"]);

                    List<Tank> tankList = new List<Tank>();
                    foreach (JToken tankToken in fuel["tankList"])
                    {
                        Tank tank = tanks.Where(x => x._name == tankToken["name"].ToString()).FirstOrDefault();
                        tankList.Add(tank);
                    }
                    fuelObject.TankList = tankList;
                    fuels.Add(fuelObject);
                }
            }
        }

        public void SaveChanges()
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
            }
        }
    }
}

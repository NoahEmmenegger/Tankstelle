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
                    tankObject.Name = tankToken["name"].ToString();
                    tankObject.AvailibleLiter = float.Parse(tankToken["availibleLiter"].ToString());
                    tankObject.MinAmount = float.Parse(tankToken["minAmount"].ToString());
                    tankObject.FuelName = tankToken["fuel"].ToString();
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
                        Tank tank = tanks.Where(x => x.Name == tankToken["name"].ToString()).FirstOrDefault();
                        tankList.Add(tank);
                    }
                    fuelObject.TankList = tankList;
                    fuels.Add(fuelObject);
                }
            }
        }

        public void SaveChanges()
        {
            string output = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                string rawJson = sr.ReadToEnd();

                JObject jObject = JObject.Parse(rawJson);
                JToken data = jObject["Data"];

                data["numberOfGasPump"] = numberOfGasPump;

                var list = JsonConvert.DeserializeObject<List<Fuel>>(data["fuels"].ToString());

                JArray jsonFuels = (JArray)data["fuels"];
                jsonFuels.Clear();
                

                foreach (Fuel fuel in fuels)
                {
                    //jsonFuels.Add(JsonConvert.SerializeObject(fuel, Formatting.Indented));
                    //var cycleJson = JObject.Parse(
                    //    "{" +
                    //        "\"name\":\"" + fuel.Name + "\"" +
                    //    "}");

                    var jsonFuel = JObject.FromObject(fuel);

                    JArray jsonTaks = (JArray)jsonFuel["TankList"];
                    jsonTaks.Clear();
                    foreach (Tank tank in fuel.TankList)
                    {
                        var tankObject = new { name = tank.Name };
                        jsonTaks.Add(JObject.FromObject(tankObject));
                    }
                    jsonFuels.Add(jsonFuel);
                }

                //data["fuels"] = JsonConvert.SerializeObject(list, Formatting.None);

                output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            }

            File.WriteAllText(@"..\..\Data\testconfig.json", output);
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Data
{
    public class ConfigurationManager
    {
        private string filePath = @"..\..\Data\config.json";
        private int numberOfGasStation;
        private List<string> fuels = new List<string>();
        private List<object> tanks = new List<object>();
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

        public int GetNumberOfGasStation()
        {
            return numberOfGasStation;
        }

        public List<string> GetFuels()
        {
            return fuels;
        }

        public List<object> GetTanks()
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
                numberOfGasStation = (int)data["numberOfGasStation"];
                var fuelsData = data["fuels"];
                foreach (string fuel in fuelsData)
                {
                    fuels.Add(fuel);
                }
                var tanksData = data["tanks"];
                foreach (object tank in tanksData)
                {
                    tanks.Add(tank);
                }
            }
        }

        public void SaveChanges()
        {
            using (StreamWriter file = File.CreateText(".\test.txt"))
            {
            }
        }
    }
}

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
        private string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "config.json");
        private int numberOfGasStation;
        private List<string> fuels;
        private List<object> tanks;
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

        public void GetDataAsJson()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string rawJson = sr.ReadToEnd();
                JObject jObject = JObject.Parse(rawJson);
                JToken data = jObject["Data"];
                numberOfGasStation = (int)data["numberOfGasStation"];
                var fuels = data["fuels"];
                var tanks = (object)data["tanks"];
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

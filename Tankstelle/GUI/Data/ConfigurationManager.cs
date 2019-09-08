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

        public int GetNumberOfGasStation()
        {
            return 2;
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

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
        private List<string> fuels = new List<string>();
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

        public void AddFuel(string fuel)
        {
            fuels.Add(fuel);
        }

        public List<string> GetFuels()
        {
            return fuels;
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
                var fuelsData = data["fuels"];
                foreach (string fuel in fuelsData)
                {
                    fuels.Add(fuel);
                }
                var tanksData = data["tanks"];
                foreach (object tank in tanksData)
                {
                    Tank tank1 = new Tank();
                    tank1._name = tank[1];
                    tanks.Add(tank1);
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

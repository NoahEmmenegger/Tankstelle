using Newtonsoft.Json;
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
        private string filePath = "./Data";
        private int numberOfGasStation;

        public int GetNumberOfGasStation()
        {
            return 2;
        }

        public void GetDataAsJson()
        {
            StreamReader sr = new StreamReader(filePath);
        }
        public void SaveChanges()
        {
            using (StreamWriter file = File.CreateText(".\test.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, numberOfGasStation);
            }
        }
    }
}

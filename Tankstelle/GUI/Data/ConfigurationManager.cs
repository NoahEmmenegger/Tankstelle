using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Data
{
    class ConfigurationManager
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
            StreamWriter sw = new StreamWriter(filePath);
        }
    }
}

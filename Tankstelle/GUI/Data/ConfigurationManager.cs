using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Data
{
    class ConfigurationManager
    {
        private string filePath = "";
        private int NumberOfGasStation;

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

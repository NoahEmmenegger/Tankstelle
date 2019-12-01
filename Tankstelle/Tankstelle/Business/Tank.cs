using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Tank
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string FuelName { get; set; }
        public float AvailibleLiter { get; set; }
        public float MinAmount { get; set; }
    }
}

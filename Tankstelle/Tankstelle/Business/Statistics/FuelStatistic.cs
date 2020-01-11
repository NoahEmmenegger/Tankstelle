using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business.Statistics
{
    public class FuelStatistic : IFuelStatistic
    {
        public Fuel Fuel { get; set; }
        public float SoldLiters { get; set; }
        public decimal Earnings { get; set; }
    }
}

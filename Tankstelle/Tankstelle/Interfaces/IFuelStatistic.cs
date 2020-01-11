using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    public interface IFuelStatistic
    {
        Fuel Fuel { get; set; }
        float SoldLiters { get; set; }
        decimal Earnings { get; set; }
    }
}

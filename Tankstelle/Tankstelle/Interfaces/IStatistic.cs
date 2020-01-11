using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Interfaces
{
    public interface IStatistic
    {
        int Monat { get; set; }
        decimal Earnings { get; set; }
        decimal Outgoings { get; set; }
        decimal MetabolicRate { get; set; }
    }
}

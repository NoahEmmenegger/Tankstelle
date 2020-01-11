using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business.Statistics
{
    public class Statistic : IStatistic
    {
        public int Monat { get; set; }
        public decimal Earnings { get; set; }
        public decimal Outgoings { get; set; }
        public decimal MetabolicRate { get; set; }
    }
}

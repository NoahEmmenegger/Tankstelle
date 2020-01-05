using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    public class Receipt : IReceipt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Fuel RelatedFuel { get; set; }
        public float RelatedLiter { get; set; }
        public decimal Sum { get; set; }
    }
}

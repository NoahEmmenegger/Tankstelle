using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Fuel RelatedFuel { get; set; }
        public int RelatedLiter { get; set; }
        public int Sum { get; set; }
    }
}

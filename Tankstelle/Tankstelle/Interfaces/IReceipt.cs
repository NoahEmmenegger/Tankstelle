using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    public interface IReceipt
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        Fuel RelatedFuel { get; set; }
        float RelatedLiter { get; set; }
        decimal Sum { get; set; }
    }
}

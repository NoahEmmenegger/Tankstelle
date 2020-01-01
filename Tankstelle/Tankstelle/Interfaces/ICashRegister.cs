using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    public interface ICashRegister
    {
        List<GasPump> GasPumpList { get; set; }
        int InsertValue { get; set; }

        int GetValueTotal();
        decimal Round(decimal toPayValue);
        bool FinishPayment(GasPump gasPump, bool verifyToPayValue = true);
        void InsertCoin(int coin);
        int GetValueInput();
        void AcceptValueInput();
        void NotAcceptValueInput();
        QuantityCoins GetChange(int outputValue);
        QuantityCoins GetQuantityCoins();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.GUI;

namespace Tankstelle.Business
{
    public class CashRegister
    {
        public List<GasPump> GasPumpList { get; set; }
        public int InsertValue { get; set; }

        public void ShowDisplay()
        {
            CashRegisterDisplay display = new CashRegisterDisplay(this);
            display.Show();
        }

        public bool FinishPayment(GasPump gasPump, bool verifyToPayValue = true)
        {
            if (verifyToPayValue)
            {
                if (gasPump.ToPayValue > 0)
                {
                    return false;
                }
            }
            GasPumpList.Find(g => g == gasPump).Refresh();
            return true;
        }
    }
}

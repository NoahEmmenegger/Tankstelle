using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Data
{
    public interface IConfigurationManager
    {
        void AddGasPump(GasPump gasPump);
        List<GasPump> GetGasPumps();

        void AddFuel(Fuel fuel);
        List<Fuel> GetFuels();

        void AddTank(Tank tank);
        List<Tank> GetTanks();

        void AddReceipt(Receipt receipt);
        List<Receipt> GetReceipts();
        void SaveGasPumpChanges();
        void SaveTankChanges();
        void SaveFuelChanges();
        void SaveReceiptChanges();
        void SaveCoinChanges();
        void SaveAllChanges();

    }
}

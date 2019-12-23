using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Data
{
    interface IConfigurationManager
    {
        void AddGasPump(GasPump gasPump);
        List<GasPump> GetGasPumps();

        void AddFuel(Fuel fuel);
        List<Fuel> GetFuels();
        Fuel GetFuelByName(string fuelName); // noch entfernen

        void AddTank(Tank tank);
        List<Tank> GetTanks();
        Tank GetTankByNumber(int number); //noch entfernen

        void AddReceipt(Receipt receipt);
        List<Receipt> GetReceipts();

        void SaveChanges();

    }
}

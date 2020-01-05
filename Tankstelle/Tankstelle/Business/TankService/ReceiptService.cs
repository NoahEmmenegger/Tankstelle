using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Data;

namespace Tankstelle.Business.TankService
{
    class ReceiptService
    {
        static IConfigurationManager configurationManager = ConfigurationManager.CreateInstance();

        public static List<Receipt> GetReceipts()
        {
            return configurationManager.GetReceipts();
        }
        public static decimal GetDayEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in configurationManager.GetReceipts().Where(x => x.Date.Date == date.Date))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        public static decimal GetWeekEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in configurationManager.GetReceipts().Where(x => GetWeekBegin(x.Date) == GetWeekBegin(date)))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        public static decimal GetMothEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in configurationManager.GetReceipts().Where(x => x.Date.Month == date.Month))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        public static decimal GetYearEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in configurationManager.GetReceipts().Where(x => x.Date.Year == date.Year))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        public static DateTime GetWeekBegin(DateTime date)
        {
            double day = Convert.ToDouble(date.DayOfWeek);
            if (day == 0)
            {
                day = 7;
            }
            day -= 1;
            var tet = date.Date.AddDays(-day);
            return tet;
        }

        public static float GetSoldLiters(DateTime date, Fuel fuel)
        {
            float litercount = 0;
            foreach (Receipt receipt in configurationManager.GetReceipts().Where(x => x.Date.Date == date.Date && x.RelatedFuel == fuel))
            {
                litercount += receipt.RelatedLiter;
            }
            return litercount;
        }
    }
}

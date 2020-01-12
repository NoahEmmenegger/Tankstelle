using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Data;

namespace Tankstelle.Business.TankService
{
    /// <summary>
    /// Erhalte weiter Informationen zu einer Quittung
    /// </summary>
    class ReceiptService
    {
        /// <summary>
        /// Holt alle Quittungen
        /// </summary>
        /// <returns></returns>
        public static List<Receipt> GetReceipts()
        {
            return GasStation.GetInstance().ReceiptList;
        }

        /// <summary>
        /// Erhalte die Einnahmen eines Tages
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static decimal GetDayEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in GasStation.GetInstance().ReceiptList.Where(x => x.Date.Date == date.Date))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        /// <summary>
        /// Erhalte die Einnahmen einer Woche
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static decimal GetWeekEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in GasStation.GetInstance().ReceiptList.Where(x => GetWeekBegin(x.Date) == GetWeekBegin(date)))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        /// <summary>
        /// Erhalte die Einnahmen eines Monates
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static decimal GetMothEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in GasStation.GetInstance().ReceiptList.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        /// <summary>
        /// Erhalte die Einnahmen eines Jahres
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static decimal GetYearEarning(DateTime date)
        {
            decimal earnings = 0;
            foreach (Receipt receipt in GasStation.GetInstance().ReceiptList.Where(x => x.Date.Year == date.Year))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        /// <summary>
        /// Erhalte das Anfangsdatum einer Woche
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Erhalte die anzahl Liter eines Tages
        /// </summary>
        /// <param name="date"></param>
        /// <param name="fuel"></param>
        /// <returns></returns>
        public static float GetSoldLiters(DateTime date, Fuel fuel)
        {
            float litercount = 0;
            foreach (Receipt receipt in GasStation.GetInstance().ReceiptList.Where(x => x.Date.Date == date.Date && x.RelatedFuel == fuel))
            {
                litercount += receipt.RelatedLiter;
            }
            return litercount;
        }
    }
}

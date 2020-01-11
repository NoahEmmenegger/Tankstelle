using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tankstelle.Business;
using Tankstelle.Business.Statistics;
using Tankstelle.Business.TankService;
using Tankstelle.Data;
using Tankstelle.Interfaces;

namespace Tankstelle.GUI.Admin
{
    /// <summary>
    /// Interaction logic for StatisticDisplay.xaml
    /// </summary>
    public partial class StatisticDisplay : Window
    {
        IConfigurationManager configurationManager = ConfigurationManager.CreateInstance();
        public StatisticDisplay()
        {
            InitializeComponent();
            List<IFuelStatistic> fuelStatistics = new List<IFuelStatistic>();
            foreach (Fuel fuel in configurationManager.GetFuels())
            {
                IFuelStatistic fuelStatistic = new FuelStatistic();
                fuelStatistic.Fuel = fuel;
                fuelStatistic.SoldLiters = ReceiptService.GetSoldLiters(DateTime.Now, fuel);
                fuelStatistic.Earnings = GetFuelEarnings(fuel, DateTime.Now);
                fuelStatistics.Add(fuelStatistic);
            }
            tanklist.ItemsSource = configurationManager.GetTanks();
            treibstoffsorten.ItemsSource = fuelStatistics;
            DayEarning.Text = "Umsatz: " + ReceiptService.GetDayEarning(DateTime.Now);
            umsatzWoche.Text = ReceiptService.GetWeekEarning(DateTime.Now.AddDays(-7)).ToString();
            umsatzdieseWoche.Text = ReceiptService.GetWeekEarning(DateTime.Now).ToString();
            umsatzMonat.Text = ReceiptService.GetMothEarning(DateTime.Now.AddMonths(-1)).ToString();
            umsatzdiesenMonat.Text = ReceiptService.GetMothEarning(DateTime.Now).ToString();
            umsatzJahr.Text = ReceiptService.GetYearEarning(DateTime.Now.AddYears(-1)).ToString();
            umsatzdiesesJahr.Text = ReceiptService.GetYearEarning(DateTime.Now).ToString();
        }

        public decimal GetFuelEarnings(Fuel fuel, DateTime date)
        {
            decimal earnings = 0;
            foreach (IReceipt receipt in configurationManager.GetReceipts().Where(x => x.Date.Date == date.Date && x.RelatedFuel == fuel))
            {
                earnings += receipt.Sum;
            }
            return earnings;
        }

        public decimal GetEarnings(int month)
        {
            if (month +1 == DateTime.Now.Month)
            {
                return ReceiptService.GetMothEarning(DateTime.Now);
            }
            else
            {
                DateTime date = DateTime.Now.AddMonths(-12 + month);
                return ReceiptService.GetMothEarning(date);
            }
        }

        public decimal GetOutgoings(int month)
        {
            return 0;
        }

        private void monatChanged(object sender, SelectionChangedEventArgs e)
        {
            IStatistic statistic = new Statistic();
            statistic.Earnings = this.GetEarnings(monat.SelectedIndex);
            statistic.Outgoings = this.GetOutgoings(monat.SelectedIndex);
            statistic.MetabolicRate = statistic.Earnings - statistic.Outgoings;
            
            this.DataContext = statistic;
            treibstoffsorten.ItemsSource = configurationManager.GetFuels();
        }
    }

}

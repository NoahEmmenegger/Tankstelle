using System;
using System.Collections.Generic;
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
using Tankstelle.Data;

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
            tanklist.ItemsSource = configurationManager.GetTanks();           
        }

        public int GetEarnings(int month)
        {
            int earnings = 0;
            foreach (Receipt receipt in configurationManager.GetReceipts())
            {
                if (receipt.Date.Month == month + 1)
                {
                    earnings += receipt.Sum;
                }
            }
            return earnings;
        }

        public int GetOutgoings(int month)
        {
            return 0;
        }

        private void monatChanged(object sender, SelectionChangedEventArgs e)
        {
            Statistic statistic = new Statistic();
            statistic.Earnings = this.GetEarnings(monat.SelectedIndex);
            statistic.Outgoings = this.GetOutgoings(monat.SelectedIndex);
            statistic.MetabolicRate = statistic.Earnings - statistic.Outgoings;

            this.DataContext = statistic;
        }
    }

    public class Statistic
    {
        public int Monat { get; set; }
        public int Earnings { get; set; }
        public int Outgoings { get; set; }
        public int MetabolicRate { get; set; }
    }
}

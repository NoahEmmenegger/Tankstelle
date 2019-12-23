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

            Statistic statistic = new Statistic();
            statistic.Umsatz = "test";
            statistic.Monat = monat.SelectedIndex;

            this.DataContext = statistic;


            
        }
    }

    public class Statistic
    {
        public int Monat { get; set; }
        public int Einnahmen { 
            get
            {
                IConfigurationManager configurationManager = ConfigurationManager.CreateInstance();
                int einnahmen = 0;
                foreach (Receipt receipt in configurationManager.GetReceipts())
                {
                    if (receipt.Date.Month == 12)
                    {
                        einnahmen += receipt.Sum;
                    }
                }
                return 0;
            }
        }
        public int Ausgaben { get; set; }
        public string Umsatz { get; set; }
    }
}

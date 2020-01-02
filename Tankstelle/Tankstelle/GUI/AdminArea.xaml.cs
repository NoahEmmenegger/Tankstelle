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
using Tankstelle.GUI.Admin;

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaction logic for AdminArea.xaml
    /// </summary>
    public partial class AdminArea : Window
    {
        public AdminArea()
        {
            InitializeComponent();
        }

        private void _btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            StatisticDisplay statisticDisplay = new StatisticDisplay();
            statisticDisplay.Show();
        }
    }
}

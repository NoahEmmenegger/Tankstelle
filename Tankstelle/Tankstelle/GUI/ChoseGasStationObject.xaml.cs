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

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für ChoseGasStationObject.xaml
    /// </summary>
    public partial class ChoseGasStationObject : Window
    {
        public ChoseGasStationObject()
        {
            InitializeComponent();
        }

        private void _btnChoseGasPump_Click(object sender, RoutedEventArgs e)
        {
            GasStation.GetFuels();
            GasStation.GetGasPumps();
            ChoseGasPump choseGasPump = new ChoseGasPump(GasStation.GasPumpList);
            choseGasPump.ShowDialog();
        }

        private void _btnChoseCashRegister_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

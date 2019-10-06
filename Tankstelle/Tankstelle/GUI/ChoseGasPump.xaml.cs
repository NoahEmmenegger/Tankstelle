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
    /// Interaktionslogik für ChoseGasPump.xaml
    /// </summary>
    public partial class ChoseGasPump : Window
    {
        public List<GasPump> Context
        {
            get
            {
                return (List<GasPump>)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public ChoseGasPump()
        {
            InitializeComponent();
            Context = GasStation.GasPumpList;
        }

        private void _btnWaehlen_Click(object sender, RoutedEventArgs e)
        {
            GasPump selectedGasPump = GasStation.GasPumpList.First(g => g == _livZapfsauulen.SelectedItem);
            selectedGasPump.OpenDisplay();
        }
    }
}

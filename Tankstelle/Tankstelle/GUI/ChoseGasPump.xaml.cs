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
            Context = GasStation.GetInstance().GasPumpList;
        }

        private void _btnWaehlen_Click(object sender, RoutedEventArgs e)
        {
            if(_livZapfsauulen.SelectedItem != null && _livZapfhaenen.SelectedItem != null)
            {
                GasPump selectedGasPump = GasStation.GetInstance().GasPumpList.First(g => g == _livZapfsauulen.SelectedItem);
                selectedGasPump.ActiveTap = selectedGasPump.TapList.First(t => t == _livZapfhaenen.SelectedItem);
                selectedGasPump.OpenDisplay();
            }
            else
            {
                MessageBox.Show("Sie müssen eine Zapfsäule und einen Zapfhan auswählen, bevor Sie fortfahren können.");
            }

        }
    }
}

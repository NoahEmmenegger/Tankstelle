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
using Tankstelle.Enums;
using Tankstelle.Interfaces;

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für ChoseGasPump.xaml
    /// </summary>
    public partial class ChoseGasPump : Window
    {
        public List<IGasPump> Context
        {
            get
            {
                return (List<IGasPump>)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public ChoseGasPump()
        {
            Context = new List<IGasPump>();
            InitializeComponent();
            foreach (var oneGasPump in GasStation.GetInstance().GasPumpList.Where(g => g.Status == Statuse.Frei).ToList())
            {
                Context.Add(oneGasPump);
            }
        }

        /// <summary>
        /// Wählt eine Zapfsäule zum Tanken aus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnWaehlen_Click(object sender, RoutedEventArgs e)
        {
            if(_livZapfsauulen.SelectedItem != null && _livZapfhaenen.SelectedItem != null)
            {
                GasPump selectedGasPump = GasStation.GetInstance().GasPumpList.First(g => g == _livZapfsauulen.SelectedItem);
                selectedGasPump.ActiveTap = selectedGasPump.TapList.First(t => t == _livZapfhaenen.SelectedItem);
                if(selectedGasPump.Status == Statuse.Frei)
                {
                    selectedGasPump.OpenDisplay();
                    GasPumpDisplay gasPumpDisplay = new GasPumpDisplay();
                    gasPumpDisplay.Context = selectedGasPump;
                    gasPumpDisplay.Show();
                }
            }
            else
            {
                MessageBox.Show("Sie müssen eine Zapfsäule und einen Zapfhan auswählen, bevor Sie fortfahren können.");
            }

        }
    }
}

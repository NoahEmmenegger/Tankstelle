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
            Generator.Generate();
            InitializeComponent();
        }

        /// <summary>
        /// Öffnet das Fenster, bei welchem die Zapfsäule ausgewählt werden kann.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnChoseGasPump_Click(object sender, RoutedEventArgs e)
        {
            ChoseGasPump choseGasPump = new ChoseGasPump();
            choseGasPump.Show();
        }

        /// <summary>
        /// Öffnet das Fenster von der Kasse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnCashRegister_Click(object sender, RoutedEventArgs e)
        {
            CashRegister cashRegister = new CashRegister();
            cashRegister.GasPumpList = GasStation.GetInstance().GasPumpList;
            cashRegister.ShowDisplay();
        }

        /// <summary>
        /// Öffnet das Fenster vom Administratorenbereich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnAdminArea_Click(object sender, RoutedEventArgs e)
        {
            AdminArea adminArea = new AdminArea();
            adminArea.Show();
        }
    }
}

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
using Tankstelle.Business.TankService;
using Tankstelle.Interfaces;

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für ChoseGasStationObject.xaml
    /// </summary>
    public partial class ChoseGasStationObject : Window
    {
        IGasStation _gasStation;
        public ChoseGasStationObject()
        {
            _gasStation  = Generator.Generate();
            foreach (Business.Message message in MessageService.CreateInstance().GetMessages())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(message.Description, message.Title, MessageBoxButton.OK);
                if (MessageBoxResult.OK == messageBoxResult)
                {
                    Application.Current.Shutdown();
                }
            }
            InitializeComponent();
        }

        /// <summary>
        /// Öffnet das Fenster, bei welchem die Zapfsäule ausgewählt werden kann.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnChoseGasPump_Click(object sender, RoutedEventArgs e)
        {
            ChoseGasPump choseGasPump = new ChoseGasPump(_gasStation);
            choseGasPump.Show();
        }

        /// <summary>
        /// Öffnet das Fenster von der Kasse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnCashRegister_Click(object sender, RoutedEventArgs e)
        {
            ICashRegister cashRegister = new CashRegister(_gasStation.GasPumpList);
            CashRegisterDisplay display = new CashRegisterDisplay(cashRegister);
            display.Show();
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

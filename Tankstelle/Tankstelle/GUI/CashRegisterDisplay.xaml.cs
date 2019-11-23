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

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für CashRegisterDisplay.xaml
    /// </summary>
    public partial class CashRegisterDisplay : Window
    {
        public CashRegister Context
        {
            get
            {
                return (CashRegister)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public CashRegisterDisplay(CashRegister cashRegister)
        {
            InitializeComponent();
            Context = cashRegister;
        }

        private void _btnWaehlen_Click(object sender, RoutedEventArgs e)
        {
            GasPump selectedGasPump = (GasPump)GasPumpComboBox.SelectedItem;
            if (selectedGasPump.Status != Statuse.Besetzt)
            {
                MessageBox.Show("Die Zapfsäule kann nicht zum bezahlen ausgewählt werden, da es der momentane Status nicht zulässt. Sie muss den Status \"Besetzt\" haben, damit sie ausgewählt werden kann.", "Nicht auswählbar", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                selectedGasPump.Status = Statuse.Bezahlen;
            }
        }

        private void _btnFertig_Click(object sender, RoutedEventArgs e)
        {
            GasPump selectedGasPump = (GasPump)GasPumpComboBox.SelectedItem;
            bool result = Context.FinishPayment(selectedGasPump);
            if (!result)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Sind Sie sicher, dass Sie die Zahlung beenden wollen? Es stehen noch Rechnungen offen.", "Zahlung beednen?", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(MessageBoxResult.OK == messageBoxResult)
                {
                    Context.FinishPayment(selectedGasPump, false);
                }
            }
        }
    }
}

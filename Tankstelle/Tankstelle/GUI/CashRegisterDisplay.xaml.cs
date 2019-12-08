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
                _tbxAnzeige.Text = "Eingabe: 0 Franken";
            }
        }

        private void _btnAbschliessen_Click(object sender, RoutedEventArgs e)
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
        
        private void Nummber_Click(object sender, RoutedEventArgs e)
        {
            string buttonContent = ((Button)sender).Content.ToString();
            if (buttonContent.Contains("Franken"))
            {
                Context.InsertCoin(Convert.ToInt32(buttonContent.Split(' ').First()) * 100);
            }
            else
            {
                Context.InsertCoin(Convert.ToInt32(buttonContent.Split(' ').First()));
            }
            var test = Context.GetValueInput();
            _tbxAnzeige.Text = $"Eingabe: {Convert.ToDouble(Context.GetValueInput()) / 100} Franken";
        }

        private void _btnInput_Click(object sender, RoutedEventArgs e)
        {

        }
        private void _btnFertig_Click(object sender, RoutedEventArgs e)
        {
            Context.AcceptValueInput();
            GasPump selectedGasPump = (GasPump)GasPumpComboBox.SelectedItem;
            int outputValue = Context.InsertValue - Convert.ToInt32((selectedGasPump.ToPayValue * 100));
            int[] outputCoins = Context.GetChange(outputValue).CountCoins();
            _tbxAnzeige.Text += "\r\nAusgabe:\r\n";
            _tbxAnzeige.Text += $"{outputCoins[0]} x 5 Rappen\r\n{outputCoins[1]} x 10 Rappen\r\n{outputCoins[2]} x 20 Rappen\r\n{outputCoins[3]} x 50 Rappen\r\n{outputCoins[4]} x 1 Franken\r\n" +
                $"{outputCoins[5]} x 2 Franken\r\n{outputCoins[6]} x 5 Franken\r\n{outputCoins[7]} x 10 Franken\r\n{outputCoins[8]} x 20 Franken\r\n{outputCoins[9]} x 50 Franken\r\n{outputCoins[10]} x 100 Franken\r\n" +
                $"{outputCoins[11]} x 200 Franken\r\n{outputCoins[12]} x 1000 Franken";
        }
    }
}

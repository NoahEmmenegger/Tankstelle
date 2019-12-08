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
        private string[] _coinCategorys = new string[] { "5 Rappen", "10 Rappen", "20 Rappen", "50 Rappen", "1 Franken", "2 Franken", "5 Franken", "10 Franken", "20 Franken", "50 Franken", "100 Franken", "200 Franken", "1000 Franken"};
        private string _zuBezahlenAusgabe;
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
                selectedGasPump.ToPayValue = Context.Round(selectedGasPump.ToPayValue);
                _tbxAnzeige.Text = _zuBezahlenAusgabe = $"Zu bezahlen: {selectedGasPump.ToPayValue} Franken\r\n";
                _tbxAnzeige.Text += "Eingabe: 0 Franken";
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
            _tbxAnzeige.Text = _zuBezahlenAusgabe;
            _tbxAnzeige.Text += $"Eingabe: {Convert.ToDouble(Context.GetValueInput()) / 100} Franken\r\n";
        }

        private void _btnInput_Click(object sender, RoutedEventArgs e)
        {

        }
        private void _btnFertig_Click(object sender, RoutedEventArgs e)
        {
            Context.AcceptValueInput();
            GasPump selectedGasPump = (GasPump)GasPumpComboBox.SelectedItem;
            int outputValue = Context.InsertValue - Convert.ToInt32((selectedGasPump.ToPayValue * 100));
            if(Context.InsertValue / 100 >= selectedGasPump.ToPayValue)
            {
                int[] outputCoins = Context.GetChange(outputValue).CountCoins();
                _tbxAnzeige.Text += "\r\nAusgabe:\r\n";
                for (int i = 0; i < outputCoins.Length; i++)
                {
                    if (outputCoins[i] > 0)
                    {
                        _tbxAnzeige.Text += $"{outputCoins[i]} x {_coinCategorys[i]}\r\n";
                    }
                }
            }
            else
            {
                _tbxAnzeige.Text += $"Es wurde noch zu wenig Geld eingeworfen. Es fehlen noch {selectedGasPump.ToPayValue - Context.InsertValue / 100} Franken.";
            }
        }
    }
}

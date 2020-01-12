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
    /// Interaktionslogik für CashRegisterDisplay.xaml
    /// </summary>
    public partial class CashRegisterDisplay : Window
    {
        private string[] _coinCategorys = new string[] { "5 Rappen", "10 Rappen", "20 Rappen", "50 Rappen", "1 Franken", "2 Franken", "5 Franken", "10 Franken", "20 Franken", "50 Franken", "100 Franken", "200 Franken", "1000 Franken" };
        private Button[] _buttons = new Button[15];
        private string _zuBezahlenAusgabe;
        /// <summary>
        /// DataContext
        /// </summary>
        public ICashRegister Context
        {
            get
            {
                return (ICashRegister)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public CashRegisterDisplay(ICashRegister cashRegister)
        {
            InitializeComponent();
            Context = cashRegister;

            _buttons[0] = _btn5Rappen;
            _buttons[1] = _btn10Rappen;
            _buttons[2] = _btn20Rappen;
            _buttons[3] = _btn50Rappen;
            _buttons[4] = _btn1Franke;
            _buttons[5] = _btn2Franken;
            _buttons[6] = _btn5Franken;
            _buttons[7] = _btn10Franken;
            _buttons[8] = _btn20Franken;
            _buttons[9] = _btn50Franken;
            _buttons[10] = _btn100Franken;
            _buttons[11] = _btn200Franken;
            _buttons[12] = _btn1000Franken;
            _buttons[13] = _btnFertig;
            _buttons[14] = _btnAbbruch;
        }
        /// <summary>
        /// Wählt eine Zapfsäule zur Bezahlung aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnWaehlen_Click(object sender, RoutedEventArgs e)
        {
            IGasPump selectedGasPump = (IGasPump)GasPumpComboBox.SelectedItem;
            if (selectedGasPump != null)
            {
                if (selectedGasPump.Status != GasPumpStatus.Besetzt)
                {
                    MessageBox.Show("Die Zapfsäule kann nicht zum bezahlen ausgewählt werden, da es der momentane Status nicht zulässt. Sie muss den Status \"Besetzt\" haben, damit sie ausgewählt werden kann.", "Nicht auswählbar", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    selectedGasPump.Status = GasPumpStatus.Bezahlen;
                    selectedGasPump.ToPayValue = Context.Round(selectedGasPump.ToPayValue);
                    _tbxAnzeige.Text = _zuBezahlenAusgabe = $"Zu bezahlen: {selectedGasPump.ToPayValue} Franken\r\n";
                    _tbxAnzeige.Text += "Eingabe: 0 Franken";
                    EnableButtons();
                }
            }
        }
        /// <summary>
        /// Schliesst die "Behandlung" von der aktuell ausgewählten Zapfsäule ab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnAbschliessen_Click(object sender, RoutedEventArgs e)
        {
            GasPump selectedGasPump = (GasPump)GasPumpComboBox.SelectedItem;
            int result = Context.FinishPayment(selectedGasPump);
            if (result == 0)
            {
                _tbxAnzeige.Text = "";
            }
            else if (result == 1)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Sind Sie sicher, dass Sie die Zahlung beenden wollen? Es stehen noch Rechnungen offen.", "Zahlung beednen?", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (MessageBoxResult.OK == messageBoxResult)
                {
                    Context.FinishPayment(selectedGasPump, false);
                    _tbxAnzeige.Text = "";
                }
            }
        }
        /// <summary>
        /// Einwurf von einem Geldstück
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            _tbxAnzeige.Text = _zuBezahlenAusgabe;
            _tbxAnzeige.Text += $"Eingabe: {Convert.ToDouble(Context.GetValueInput()) / 100} Franken\r\n";
        }
        /// <summary>
        /// Schliesst die Zahlung ab, sofern genug Geld eingeworfen wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnFertig_Click(object sender, RoutedEventArgs e)
        {
            GasPump selectedGasPump = (GasPump)GasPumpComboBox.SelectedItem;
            int[] outputCoins = new int[1];
            try
            {
                outputCoins = Context.FinishInput(selectedGasPump);
                if (outputCoins[0] == -1)
                {
                    _tbxAnzeige.Text += $"\r\nEs wurde noch zu wenig Geld eingeworfen. Es fehlen noch {selectedGasPump.ToPayValue - decimal.Parse(Context.GetValueInput().ToString()) / 100} Franken.";
                }
                else
                {
                    _tbxAnzeige.Text += "\r\nAusgabe:\r\n";
                    RenderNumberOfCoins(outputCoins);
                    DisableButtons();

                    IReceipt receipt = Context.CreateReceipt(selectedGasPump);
                    selectedGasPump.ToPayValue = 0;
                    ReceiptDisplay receiptDisplay = new ReceiptDisplay();
                    receiptDisplay.Context = receipt;
                    receiptDisplay.Show();
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Es kann leider kein Rückgeld gegeben werden."))
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        /// <summary>
        /// Gibt den Inhalt von der Kasse aus. Wieviel Geld in der Kasse ist und von welchen Münzen wievviel vorhanden ist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnContent_Click(object sender, RoutedEventArgs e)
        {
            int totalValue = Context.GetValueTotal();
            _tbxAnzeige.Text += $"\r\n\r\nDer Wert von allen Münzen im Automat hat folgenden Wert:\r\n{totalValue / 100} Fr.";
            int[] totalCoins = Context.GetQuantityCoins().CountCoins();
            _tbxAnzeige.Text += "\r\nFolgende Münzen sind im Apperat enthalten:\r\n";
            RenderNumberOfCoins(totalCoins);
        }
        /// <summary>
        /// Methode wird ausgelöst wenn die Bezahlung abgebrochen werden soll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnAbbruch_Click(object sender, RoutedEventArgs e)
        {
            Context.NotAcceptValueInput();
            _tbxAnzeige.Text = $"Die Bezahlung wurde abgebrochen. Das Geld wurde zurückerstattet.";
        }
        /// <summary>
        /// Stellt die mitgegebenen Münzen korrekt im GUI dar
        /// </summary>
        /// <param name="coins"></param>
        private void RenderNumberOfCoins(int[] coins)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i] > 0)
                {
                    _tbxAnzeige.Text += $"{coins[i]} x {_coinCategorys[i]}\r\n";
                }
            }
        }
        /// <summary>
        /// Aktiviert die Buttons, welche es zum Zahlen braucht
        /// </summary>
        private void EnableButtons()
        {
            foreach (var oneButton in _buttons)
            {
                oneButton.IsEnabled = true;
            }
        }
        /// <summary>
        /// Graut die Buttons aus, welche es zum Zahlen braucht.
        /// </summary>
        private void DisableButtons()
        {
            foreach (var oneButton in _buttons)
            {
                oneButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// Löscht alles ausser der Anzeige
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnClear_Click(object sender, RoutedEventArgs e)
        {
            _tbxAnzeige.Text = "";
        }
    }
}

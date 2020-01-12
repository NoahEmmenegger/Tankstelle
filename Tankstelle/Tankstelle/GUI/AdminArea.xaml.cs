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
using Tankstelle.GUI.Admin;

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaction logic for AdminArea.xaml
    /// </summary>
    public partial class AdminArea : Window
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public AdminArea()
        {
            InitializeComponent();
            List<Message> messages = new List<Message>();

            foreach (Tank tank in GasStation.GetInstance().TankList)
            {
                //Schaut ob genügend Treibstoff im Tank ist
                if (!TankService.HasEnoughInTank(tank))
                {
                    Message message = new Message();
                    message.Status = Status.Warning;
                    message.Description = "Das Tank minimum wurde im Tank " + tank.Name + " erreicht";
                    messages.Add(message);
                }

                //Vergleicht mit dem letzten Jahr und schaut ob genügend Treibstoff im Tank ist
                if (TankService.AdjustTankMinimum(tank))
                {
                    Message message = new Message();
                    message.Status = Status.Warning;
                    message.Description = "Letztes Jahr wurde im Tank: " + tank.Name + " der Treibstoff sehr knapp";
                    messages.Add(message);
                }
            }

            messageList.ItemsSource = messages; 
        }

        /// <summary>
        /// Knopf für Statistik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            StatisticDisplay statisticDisplay = new StatisticDisplay();
            statisticDisplay.Show();
        }
    }

    public class Message
    {
        public string Description { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Achtung = 0,
        Warning = 1,
        Error = 2
    }
}

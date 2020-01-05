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
        public AdminArea()
        {
            InitializeComponent();
            TankService tankService = new TankService();
            List<Message> messages = new List<Message>();

            foreach (Tank tank in GasStation.GetInstance().TankList)
            {
                if (!tankService.HasEnoughInTank(tank))
                {
                    Message message = new Message();
                    message.Status = Status.Warning;
                    message.Description = "Nicht genügend Treibstoff in Tank " + tank.Name;
                    messages.Add(message);
                }

                if (true)
                {
                    Message message = new Message();
                    message.Status = Status.Warning;
                    message.Description = "Mindestmenge muss für " + tank.Name + " angepasst werden";
                    messages.Add(message);
                }
            }

            messageList.ItemsSource = messages; 
        }

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

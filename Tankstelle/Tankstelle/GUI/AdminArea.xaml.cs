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
using Tankstelle.Enums;
using Tankstelle.GUI.Admin;
using Tankstelle.Interfaces;

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
            List<IAdminMessage> messages = new List<IAdminMessage>();

            foreach (Tank tank in GasStation.GetInstance().TankList)
            {
                //Schaut ob genügend Treibstoff im Tank ist
                if (!TankService.HasEnoughInTank(tank))
                {
                    IAdminMessage message = new AdminMessage();
                    message.Status = MessageStatus.Warning;
                    message.Description = "Nicht genügend Treibstoff in Tank " + tank.Name;
                    messages.Add(message);
                }

                //Vergleicht mit dem letzten Jahr und schaut ob genügend Treibstoff im Tank ist
                if (!TankService.AdjustTankMinimum(tank))
                {
                    IAdminMessage message = new AdminMessage();
                    message.Status = MessageStatus.Warning;
                    message.Description = "Mindestmenge muss für " + tank.Name + " angepasst werden";
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
}

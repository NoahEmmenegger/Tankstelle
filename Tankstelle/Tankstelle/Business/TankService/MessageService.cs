using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tankstelle.Data;

namespace Tankstelle.Business.TankService
{
    class MessageService
    {
        public static void AddShutDownMessage(string title, string description)
        {
            if (MessageBox.Show(description, title, MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        public static void AddMessage(string title, string description)
        {
            if (MessageBox.Show(description, title, MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
    }
}

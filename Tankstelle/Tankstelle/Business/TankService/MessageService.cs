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
        public static void AddMessage(string title, string description)
        {
            Message message = new Message();
            message.Title = title;
            message.Description = description;
            MessageBoxResult messageBoxResult = MessageBox.Show(description, title, MessageBoxButton.OK);
            if (MessageBoxResult.OK == messageBoxResult)
            {
                Application.Current.Shutdown();
            }
        }
    }
}

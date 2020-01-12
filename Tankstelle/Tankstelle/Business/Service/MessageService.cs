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
        /// <summary>
        /// Zeigt eine fatal Error Message an. Nach bestätigung wird das Program geschlossen
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public static void AddFatalErrorMessage(string title, string description)
        {
            if (MessageBox.Show(description, title, MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        /// <summary>
        /// Zeigt eine Error Message an.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public static void AddErrorMessage(string title, string description)
        {
            MessageBox.Show(description, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Zeit normale Message an
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public static void AddMessage(string title, string description)
        {
            MessageBox.Show(description, title, MessageBoxButton.OK);
        }

        /// <summary>
        /// Zeigt warning Message an
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public static void AddWarningMessage(string title, string description)
        {
            MessageBox.Show(description, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

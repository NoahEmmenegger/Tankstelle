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
    /// Interaktionslogik für GasPumpDisplay.xaml
    /// </summary>
    public partial class GasPumpDisplay : Window
    {
        /// <summary>
        /// DataContext
        /// </summary>
        public IGasPump Context
        {
            get
            {
                return (IGasPump)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public GasPumpDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Startet die Abschliessung vom Tankvorgang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnFertig_Click(object sender, RoutedEventArgs e)
        {
            Context.FinishRefuel();
            this.Close();
        }

        /// <summary>
        /// Startet den Tankvorgang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnTanken_Click(object sender, RoutedEventArgs e)
        {
            Context.StartRefuel();
        }
        /// <summary>
        /// Unterbricht den Tankvorgang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnStopp_Click(object sender, RoutedEventArgs e)
        {
            Context.StopRefuel();
        }
        /// <summary>
        /// Wird ausgefüht, wenn das Fenster geschlossen werden soll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(Context.Status == GasPumpStatus.Tankend)
            {
                if(Context.ToPayValue != 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Sie dürfen das Fenster so nicht schliessen, da Sie noch etwas bezahlen müssen. Schliessen Sie es mit dem Fertigbutton", "Schliessung verweigert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Context.Status = GasPumpStatus.Frei;
                }
            }
        }
    }
}

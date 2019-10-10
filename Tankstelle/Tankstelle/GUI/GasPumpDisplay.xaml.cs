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

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für GasPumpDisplay.xaml
    /// </summary>
    public partial class GasPumpDisplay : Window
    {
        public GasPump Context
        {
            get
            {
                return (GasPump)DataContext;
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
    }
}

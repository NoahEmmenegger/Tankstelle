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
using Tankstelle.Interfaces;

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für ReceiptDisplay.xaml
    /// </summary>
    public partial class ReceiptDisplay : Window
    {
        public ReceiptDisplay()
        {
            InitializeComponent();
        }

        public IReceipt Context
        {
            get
            {
                return (IReceipt)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}

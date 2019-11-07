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
using Tankstelle.Data;
using Tankstelle.Business;

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für ChoseCashRegister.xaml
    /// </summary>
    public partial class ChoseCashRegister : Window
    {
        public List<CashRegister> Context
        {
            get
            {
                return (List<CashRegister>)DataContext;
            }
            set
            {
                DataContext = value;
            }
        } 
        public ChoseCashRegister()
        {
            InitializeComponent();
            Context = GasStation.GetInstance().CashRegisterList;
        }
    }
}

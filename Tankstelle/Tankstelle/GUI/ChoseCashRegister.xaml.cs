﻿using System;
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

namespace Tankstelle.GUI
{
    /// <summary>
    /// Interaktionslogik für ChoseCashRegister.xaml
    /// </summary>
    public partial class ChoseCashRegister : Window
    {
        public ChoseCashRegister()
        {
            InitializeComponent();
            ConfigurationManager configuration = ConfigurationManager.CreateInstance();
            configuration.GetDataAsJson();
        }
    }
}

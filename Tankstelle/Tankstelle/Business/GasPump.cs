﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;

namespace Tankstelle.Business
{
    public class GasPump
    {
        /// <summary>
        /// Legt fest ob Zapfsäule in Gebrauch ist.
        /// </summary>
        private bool _isLocked = false;
        public int GasPumpNumber { get; set; }
        public decimal ToPayValue { get; set; }
        public ActiveFuelPropertys ActiveFuel { get; set; }
        public void OpenDisplay()
        {

        }
        public GasPump(int gasPumpNumber)
        {
            GasPumpNumber = gasPumpNumber;
        }
    }
}

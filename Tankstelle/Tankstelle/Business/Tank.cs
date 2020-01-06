using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Tank
    {
        /// <summary>
        /// Die eindeutige Nummer vom Tank
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Der Name vom Tank
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Der Name von der Treibstoffsorte, welche dieser Tank beinhaltet
        /// </summary>
        public string FuelName { get; set; }
        /// <summary>
        /// Die Liter, welche sich im Tank befinden und getankt werden können
        /// </summary>
        public float AvailibleLiter { get; set; }
        /// <summary>
        /// Die Anzahl Liter, welche in den Tank passen.
        /// </summary>
        public float VolumeLiter { get; set; }
        /// <summary>
        /// Die Mindestfüllmenge vom Tank
        /// </summary>
        public float MinAmount { get; set; }
    }
}

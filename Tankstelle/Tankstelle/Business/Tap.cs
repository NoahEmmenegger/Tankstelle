using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    /// <summary>
    /// Zapfhahn
    /// </summary>
    public class Tap
    {
        /// <summary>
        /// Definiert ob der Zapfhan gerade gelockt ist oder gebraucht werden kann.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Treibstoffart, welche mit dem Zapfhahn getankt werden kann.
        /// </summary>
        public Fuel Fuel { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fuel"></param>
        public Tap(Fuel fuel)
        {
            Fuel = fuel;
        }
    }
}

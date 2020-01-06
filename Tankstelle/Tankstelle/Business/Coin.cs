using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Coin
    {
        /// <summary>
        /// Wert vom der Münze/Note
        /// </summary>
        private int _value;

        public Coin(int value)
        {
            _value = value;
        }

        /// <summary>
        /// Gibt den Wert von der Münze zurück
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return _value;
        }
    }
}

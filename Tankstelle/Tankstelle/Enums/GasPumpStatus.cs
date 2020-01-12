using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Enums
{
    /// <summary>
    /// Status einer Zapfsäule
    /// </summary>
    public enum GasPumpStatus
    {
        Geschlossen = 0,
        Frei = 1,
        Tankend = 2,
        Besetzt = 3,
        Bezahlen = 4
    }
}

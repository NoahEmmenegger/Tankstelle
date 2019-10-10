using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.GUI;

namespace Tankstelle.Business
{
    public class GasPump
    {
        private GasPumpDisplay _display = new GasPumpDisplay();        
        private Tap _activeTap;

        /// <summary>
        /// Liste mit allen Zapfhähnen, welche es bei dieser Zapfsäule gibt.
        /// </summary>
        public List<Tap> TapList { get; set; } = new List<Tap>();

        /// <summary>
        /// Der Zapfhahn, welcher momentan im Gebrauch ist
        /// </summary>
        public Tap ActiveTap
        {
            get
            {
                return _activeTap;
            }
            set
            {
                TapList.ForEach(t => t.IsLocked = true);
                _activeTap = value;
            }
        }

        /// <summary>
        /// Nummer von der Zapfsäule
        /// </summary>
        public int GasPumpNumber { get; set; }

        /// <summary>
        /// Wert welcher bei dieser Zapfäule bezahlt werden muss.
        /// </summary>
        /// 
        public decimal ToPayValue { get; set; }
        /// <summary>
        /// Öffnet das Fenster zur Zapfsäule
        /// </summary>
        /// 
        public void OpenDisplay()
        {
            _display.Context = this;
            _display.Show();
        }
        public GasPump(int gasPumpNumber)
        {
            GasPumpNumber = gasPumpNumber;
            foreach (var oneFuel in GasStation.FuelList)
            {
                TapList.Add(new Tap(oneFuel));
            }
        }
    }
}

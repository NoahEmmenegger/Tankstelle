using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tankstelle.GUI;

namespace Tankstelle.Business
{
    public class GasPump : INotifyPropertyChanged
    {
        private GasPumpDisplay _display = new GasPumpDisplay();        
        private Tap _activeTap;
        private decimal toPayValue;
        private double liter;
        private Timer timer = new Timer();

        public event PropertyChangedEventHandler PropertyChanged;


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
        public decimal ToPayValue
        {
            get
            {
                return toPayValue;
            }
            set
            {
                toPayValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ToPayValue"));
            }
        }

        /// <summary>
        /// Anzahl der bereits getankten Liter an dieser Zapfsäule vom akktuellen Kunden
        /// </summary>
        public double Liter
        {
            get
            {
                return liter;
            }
            set
            {
                liter = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Liter"));
            }
        }
        /// <summary>
        /// Öffnet das Fenster zur Zapfsäule
        /// </summary>
        /// 

        public GasPump(int gasPumpNumber)
        {
            GasPumpNumber = gasPumpNumber;
            foreach (var oneFuel in GasStation.GetInstance().FuelList)
            {
                TapList.Add(new Tap(oneFuel));
            }
        }
        public void OpenDisplay()
        {
            _display.Context = this;
            _display.Show();
        }
        public void StartRefuel()
        {
            timer.Interval = 1000;
            timer.Elapsed += Refuel;
            timer.Start();
        }

        public void StopRefuel()
        {
            timer.Stop();
        }

        public void Refuel(Object source, ElapsedEventArgs e)
        {
            Liter = Liter + 0.25;
            ToPayValue = Convert.ToDecimal(Liter) * ActiveTap.Fuel.PricePerLiter;
        }
    }
}

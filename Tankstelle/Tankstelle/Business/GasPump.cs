using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tankstelle.Enums;
using Tankstelle.GUI;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    public class GasPump : INotifyPropertyChanged, IGasPump
    {
        /// <summary>
        /// Fenster (GUI) von der Zapfsäule
        /// </summary>
        private GasPumpDisplay _display;
        private Tap _activeTap;
        private decimal toPayValue;
        private double liter;
        private Statuse _status;
        private Timer timer = new Timer();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Liste mit allen Zapfhähnen, welche es bei dieser Zapfsäule gibt.
        /// </summary>
        public List<Tap> TapList { get; set; } = new List<Tap>();
        /// <summary>
        /// Der Zapfhahn von der Zapfsäule, welcher momentan im Gebrauch ist
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
        /// Zeigt auf in welchem Status sich dieser Zapfhan gerade befindet, ob z.B. getankt wird.
        /// </summary>
        public Statuse Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
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
            Status = Statuse.Frei;
        }
        /// <summary>
        /// Öffnet das Fenster von der Zapfsäule
        /// </summary>
        public void OpenDisplay()
        {
            try
            {
                Status = Statuse.Tankend;
                _display = new GasPumpDisplay();
                _display.Context = this;
                _display.Show();
            }
            catch(Exception ex)
            {
                //TODO Exception Handling 
            }

        }
        /// <summary>
        /// Startet das Tanken
        /// </summary>
        public void StartRefuel()
        {
            if(Status != Statuse.Besetzt)
            {
                timer.Interval = 1000;
                timer.Elapsed += Refuel;
                timer.Start();
            }
        }
        /// <summary>
        /// Stop den Tankvorgang
        /// </summary>
        public void StopRefuel()
        {
            timer.Stop();
        }
        /// <summary>
        /// Schliesst den Tankvorgang ab
        /// </summary>
        public void FinishRefuel()
        {           
            Status = Statuse.Besetzt;
            timer.Stop();
            _display.Close();
        }
        /// <summary>
        /// Berechnet das Tanken. Wieviel Liter Treibstoff bezogen wird und wie teuer es ist
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void Refuel(Object source, ElapsedEventArgs e)
        {
            Liter = Liter + 0.25;
            ToPayValue = Convert.ToDecimal(Liter) * ActiveTap.Fuel.PricePerLiter;
        }
        /// <summary>
        /// Setzt die Zapfsäule auf den Zustand zurück, dass wieder getankt werden kann.
        /// </summary>
        public void Refresh()
        {
            Status = Statuse.Frei;
            Liter = 0;
            ToPayValue = 0;
        }
    }
}

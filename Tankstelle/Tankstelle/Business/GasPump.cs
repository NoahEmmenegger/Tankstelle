using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tankstelle.Business.TankService;
using Tankstelle.Enums;
using Tankstelle.GUI;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    /// <summary>
    /// Zapfsäule
    /// </summary>
    public class GasPump : INotifyPropertyChanged, IGasPump
    {
        #region private Felder
        /// <summary>
        /// Der Zapfhahn von der Zapfsäule, welcher momentan im Gebrauch ist
        /// </summary>
        private Tap _activeTap;
        /// <summary>
        /// Wert welcher bei dieser Zapfäule bezahlt werden muss.
        /// </summary>
        /// 
        private decimal toPayValue;
        /// <summary>
        /// Anzahl der bereits getankten Liter an dieser Zapfsäule vom akktuellen Kunden
        /// </summary>
        private double liter = 0;
        /// <summary>
        /// Zeigt auf in welchem Status sich dieser Zapfhan gerade befindet, ob z.B. getankt wird.
        /// </summary>
        private GasPumpStatus _status;
        /// <summary>
        /// Timer, welcher den Rythmus vom Tanken angibt.
        /// </summary>
        private Timer timer = new Timer();
        #endregion

        #region public Properties
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
        public GasPumpStatus Status
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
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Konstruktor
        public GasPump(int gasPumpNumber)
        {
            GasPumpNumber = gasPumpNumber;
            foreach (var oneFuel in GasStation.GetInstance().FuelList)
            {
                TapList.Add(new Tap(oneFuel));
            }
            Status = GasPumpStatus.Frei;
        }
        #endregion

        #region Methoden

        /// <summary>
        /// Bereitet die Zapfsäule für das Tanken vor.
        /// </summary>
        /// <param name="selectedTap">Zapfhahan mit welche getankt werden soll.</param>
        /// <returns>Gibt an ob Sie erfolgreich vorbereitet werden konnte.</returns>
        public bool PrepareForRefuel(Tap selectedTap)
        {
            if (Status == GasPumpStatus.Frei)
            {
                Status = GasPumpStatus.Tankend;
                ActiveTap = selectedTap;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Startet das Tanken
        /// </summary>
        public void StartRefuel()
        {
            if (Status != GasPumpStatus.Besetzt)
            {
                timer.Interval = 1000;
                timer.Elapsed += Refuel;
                timer.Start();
            }
        }
        /// <summary>
        /// Stopt den Tankvorgang
        /// </summary>
        public void StopRefuel()
        {
            timer.Stop();
            GasStation.GetInstance().UpdateTanks();
        }
        /// <summary>
        /// Schliesst den Tankvorgang ab
        /// </summary>
        public void FinishRefuel()
        {
            Status = GasPumpStatus.Besetzt;
            timer.Stop();
        }
        /// <summary>
        /// Berechnet das Tanken. Wieviel Liter Treibstoff bezogen wird und wie teuer es ist
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void Refuel(Object source, ElapsedEventArgs e)
        {
            try
            {
                Tank tank = _activeTap.Fuel.TankList.First(t => t.VolumeLiter >= 0.25);
                if (tank != null)
                {
                    tank.VolumeLiter = tank.VolumeLiter - 0.25f;
                }
                else
                {
                    StopRefuel();
                    MessageService.AddWarningMessage("Zu wenig Treibstoff", "Es ist zu wenig Treibstoff vorhanden, um weiter zu tanken können. Gehen Sie bitte an die Kasse und zahlen Sie das bereits getankte.");
                    return;
                }
            }
            catch(Exception ex)
            {
                StopRefuel();
                MessageService.AddErrorMessage("Fehler", ex.Message);
                return;
            }
            Liter = Liter + 0.25;
            ToPayValue = Convert.ToDecimal(Liter) * ActiveTap.Fuel.PricePerLiter;
        }
        /// <summary>
        /// Setzt die Zapfsäule auf den Zustand zurück, dass wieder getankt werden kann.
        /// </summary>
        public void Refresh()
        {
            Status = GasPumpStatus.Frei;
            Liter = 0;
            ToPayValue = 0;
        }
        #endregion
    }
}

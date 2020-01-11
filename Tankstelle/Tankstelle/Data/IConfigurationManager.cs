using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Data
{
    /// <summary>
    /// Erhalte die Daten als Objekt, welche im config stehen und speichere Änderungen ab
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Neue Zapfsäulen hinzufügen
        /// </summary>
        /// <param name="gasPump"></param>
        void AddGasPump(GasPump gasPump);
        /// <summary>
        /// Alle Zapfsäulen erhalten
        /// </summary>
        /// <returns></returns>
        List<GasPump> GetGasPumps();
        /// <summary>
        /// Neue Treibstoffsorten hinzufügenss
        /// </summary>
        /// <param name="fuel"></param>
        void AddFuel(Fuel fuel);
        /// <summary>
        /// Erhalte alle Treibstoffsorten
        /// </summary>
        /// <returns></returns>
        List<Fuel> GetFuels();
        /// <summary>
        /// Neuer Tank hinzufügen
        /// </summary>
        /// <param name="tank"></param>
        void AddTank(Tank tank);
        /// <summary>
        /// Erhalte alle Tanks
        /// </summary>
        /// <returns></returns>
        List<Tank> GetTanks();
        /// <summary>
        /// Neue Quittung hinzufügen
        /// </summary>
        /// <param name="receipt"></param>
        void AddReceipt(Receipt receipt);
        /// <summary>
        /// Erhalte Quittungen
        /// </summary>
        /// <returns></returns>
        List<Receipt> GetReceipts();
        /// <summary>
        /// Neue Münze hinzufügen
        /// </summary>
        /// <param name="coin"></param>
        void AddCoin(Coin coin);
        /// <summary>
        /// Erhalte alle Münzen
        /// </summary>
        /// <returns></returns>
        List<Coin> GetCoins();
        /// <summary>
        /// speichert alle Zapfsäulen welche in diesem Objekt existieren
        /// </summary>
        void SaveGasPumpChanges();
        /// <summary>
        /// speichert alle Tanks welche in diesem Objekt existieren
        /// </summary>
        void SaveTankChanges();
        /// <summary>
        /// speichert alle Treibstoffsorten welche in diesem Objekt existieren
        /// </summary>
        void SaveFuelChanges();
        /// <summary>
        /// speichert alle Quittungen welche in diesem Objekt existieren
        /// </summary>
        void SaveReceiptChanges();
        /// <summary>
        /// speichert alle Münzen welche in diesem Objekt existieren
        /// </summary>
        void SaveCoinChanges();
        /// <summary>
        /// Schreibt alle Änderungen in ein json
        /// </summary>
        void SaveAllChanges();

    }
}

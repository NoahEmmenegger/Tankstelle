using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business;

namespace Tankstelle.Interfaces
{
    public interface ICashRegister
    {
        List<GasPump> GasPumpList { get; set; }

        /// <summary>
        /// Gibt den totalen Geldwert von allen Münzen in der Kasse zurück.
        /// </summary>
        /// <returns>Totalen Geldwert von allen Münzen in der Kasse</returns>
        int GetValueTotal();
        /// <summary>
        /// Rundet einen Wert, dass es ein gültiger Frankenbetrag ist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        decimal Round(decimal toPayValue);
        /// <summary>
        /// Schliesst die Zahlung ab, sofern die Schulden beglichen wurden
        /// </summary>
        /// <param name="gasPump">Zapfsäule bei, welcher die Zahlung abgeschlossen werden soll</param>
        /// <param name="verifyToPayValue">Gibt an ob die Bezahlung auch abgeschlossen werden soll, wenn noch eine Rechnung offen ist. Bei false ist es egal ob noch eine Rechnung offen ist.</param>
        /// <returns>Gibt an ob die Rechnung abgeschlossen werden konnte oder nicht.</returns>
        int FinishPayment(GasPump gasPump, bool verifyToPayValue = true);
        /// <summary>
        /// Schliesst die Eingabe vom Geld ab und gibt das Rückgeld
        /// </summary>
        /// <param name="gasPump"></param>
        /// <returns>Das Rückgeld oder -1 wenn noch zu wenig Geld eingeworfen wurde</returns>
        int[] FinishInput(GasPump gasPump);
        /// <summary>
        /// Findet heraus was für eine Münze eingeworfen wurde und fügt Sie dem entsprechenden CoinContainer hinzu.
        /// </summary>
        /// <param name="coin">Eingeworfene Münze</param>
        void InsertCoin(int coin);
        /// <summary>
        /// Gibt zurück wieviel Geld bereits eingeworfen wurde.
        /// </summary>
        /// <returns>Der Wert vom eingeworfenen Geld</returns>
        int GetValueInput();
        /// <summary>
        /// Füllt das eingeworfene Geld vom Kunden in die entsprechenden CoinContainers ab.
        /// </summary>
        void AcceptValueInput();
        /// <summary>
        /// Löscht die Münzen aus der Variable<para name="insertCoins"/> 
        /// </summary>
        void NotAcceptValueInput();
        /// <summary>
        /// Gibt das Rückgeld dem Kunden heraus
        /// </summary>
        /// <param name="outputValue">Betrag, welcher ausbezahlt werden muss</param>
        /// <returns>Die Anzahl von den verschiedenen Münzen, welche ausgegeben werden müssen</returns>
        QuantityCoins GetChange(int outputValue);
        /// <summary>
        /// Fragt die effektive Anzahl von Münzen im Automaten ab.
        /// </summary>
        /// <returns></returns>
        QuantityCoins GetQuantityCoins();
        /// <summary>
        /// Erstellt eine Rechnung
        /// </summary>
        /// <param name="gasPump"></param>
        /// <returns></returns>
        Receipt CreateReceipt(GasPump gasPump);
    }
}

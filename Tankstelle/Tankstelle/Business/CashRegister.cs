using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business.TankService;
using Tankstelle.GUI;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    /// <summary>
    /// Ist die Kasse. Beinhaltet alle Methoden und Variablen, welche zum Bezahlen benötigt werden.
    /// </summary>
    public class CashRegister : ICashRegister
    {
        #region private Felder
        /// <summary>
        /// Maximale Anzahl Münzen, welche im Automat sein können.
        /// </summary>
        private readonly int maxCoins;
        /// <summary>
        /// Die Münzen welche in den Automat gegeben wurden
        /// </summary>
        private List<Coin> insertCoins = new List<Coin>();
        /// <summary>
        /// Ein Array für die CoinContainer
        /// </summary>
        private CoinContainer[] containers = new CoinContainer[13];
        /// <summary>
        /// Der insgesamte Wert von den Münzen, welche eingeworfen worden sind.
        /// </summary>
        private int insertValue = 0;
        #endregion

        #region public Properties
        /// <summary>
        /// Liste von allen Zapfsäulen
        /// </summary>
        public List<GasPump> GasPumpList { get; set; }
        #endregion

        #region Konstruktor
        public CashRegister(List<GasPump> gasPumps)
        {
            GasPumpList = gasPumps;
            containers[0] = new CoinContainer(5, 100);
            containers[1] = new CoinContainer(10, 100);
            containers[2] = new CoinContainer(20, 100);
            containers[3] = new CoinContainer(50, 100);
            containers[4] = new CoinContainer(100, 50);
            containers[5] = new CoinContainer(200, 50);
            containers[6] = new CoinContainer(500, 30);
            containers[7] = new CoinContainer(1000, 50);
            containers[8] = new CoinContainer(2000, 50);
            containers[9] = new CoinContainer(5000, 50);
            containers[10] = new CoinContainer(10000, 40);
            containers[11] = new CoinContainer(20000, 40);
            containers[12] = new CoinContainer(100000, 40);
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Schliesst die Zahlung ab, sofern die Schulden beglichen wurden
        /// </summary>
        /// <param name="gasPump">Zapfsäule bei, welcher die Zahlung abgeschlossen werden soll</param>
        /// <param name="verifyToPayValue">Gibt an ob die Bezahlung auch abgeschlossen werden soll, wenn noch eine Rechnung offen ist. Bei false ist es egal ob noch eine Rechnung offen ist.</param>
        /// <returns>Gibt an ob die Rechnung abgeschlossen werden konnte oder nicht.</returns>
        public int FinishPayment(GasPump gasPump, bool verifyToPayValue = true)
        {
            if (gasPump != null)
            {
                if (verifyToPayValue)
                {
                    if (gasPump.ToPayValue > 0)
                    {
                        return 1;
                    }
                }
                GasPumpList.Find(g => g == gasPump).Refresh();
                return 0;
            }
            return 2;
        }
        /// <summary>
        /// Schliesst die Eingabe vom Geld ab und gibt das Rückgeld
        /// </summary>
        /// <param name="gasPump"></param>
        /// <returns>Das Rückgeld oder -1 wenn noch zu wenig Geld eingeworfen wurde</returns>
        public int[] FinishInput(GasPump gasPump)
        {
            AcceptValueInput();
            int inputValue = GetValueInput();
            int outputValue = inputValue - Convert.ToInt32((gasPump.ToPayValue * 100));
            if (decimal.Parse(inputValue.ToString()) / 100 >= gasPump.ToPayValue)
            {
                int[] outputCoins = new int[1];
                outputCoins = GetChange(outputValue).CountCoins();
                GasStation.GetInstance().DeleteCoins();
                List<Coin> coins = new List<Coin>();
                foreach (var oneContainer in containers)
                {
                    coins.AddRange(oneContainer.GetCoins().Where(c => c != null));
                }
                GasStation.GetInstance().UpdateCoins(coins);
                return outputCoins;
            }
            else
            {
                return new int[] { -1 };
            }
        }

        /// <summary>
        /// Findet heraus was für eine Münze eingeworfen wurde und fügt Sie dem entsprechenden CoinContainer hinzu.
        /// </summary>
        /// <param name="coin">Eingeworfene Münze</param>
        public void InsertCoin(int coin)
        {
            Coin inputCoin;
            switch (coin)
            {
                case 5:
                    inputCoin = new Coin(5);
                    insertCoins.Add(inputCoin);
                    insertValue += 5;
                    break;
                case 10:
                    inputCoin = new Coin(10);
                    insertCoins.Add(inputCoin);
                    insertValue += 10;
                    break;
                case 20:
                    inputCoin = new Coin(20);
                    insertCoins.Add(inputCoin);
                    insertValue += 20;
                    break;
                case 50:
                    inputCoin = new Coin(50);
                    insertCoins.Add(inputCoin);
                    insertValue += 50;
                    break;
                case 100:
                    inputCoin = new Coin(100);
                    insertCoins.Add(inputCoin);
                    insertValue += 100;
                    break;
                case 200:
                    inputCoin = new Coin(200);
                    insertCoins.Add(inputCoin);
                    insertValue += 200;
                    break;
                case 500:
                    inputCoin = new Coin(500);
                    insertCoins.Add(inputCoin);
                    insertValue += 500;
                    break;
                case 1000:
                    inputCoin = new Coin(1000);
                    insertCoins.Add(inputCoin);
                    insertValue += 1000;
                    break;
                case 2000:
                    inputCoin = new Coin(2000);
                    insertCoins.Add(inputCoin);
                    insertValue += 2000;
                    break;
                case 5000:
                    inputCoin = new Coin(5000);
                    insertCoins.Add(inputCoin);
                    insertValue += 5000;
                    break;
                case 10000:
                    inputCoin = new Coin(10000);
                    insertCoins.Add(inputCoin);
                    insertValue += 10000;
                    break;
                case 20000:
                    inputCoin = new Coin(20000);
                    insertCoins.Add(inputCoin);
                    insertValue += 20000;
                    break;
                case 100000:
                    inputCoin = new Coin(100000);
                    insertCoins.Add(inputCoin);
                    insertValue += 100000;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gibt zurück wieviel Geld bereits eingeworfen wurde.
        /// </summary>
        /// <returns>Der Wert vom eingeworfenen Geld</returns>
        public int GetValueInput()
        {
            return insertValue;
        }

        /// <summary>
        /// Füllt das eingeworfene Geld vom Kunden in die entsprechenden CoinContainers ab.
        /// </summary>
        public void AcceptValueInput()
        {
            foreach (var oneCoin in insertCoins)
            {
                var oneCoinValue = oneCoin.GetValue();
                foreach (var oneCoinContainer in containers)
                {
                    if (oneCoinContainer.GetCoinsValue() == oneCoinValue)
                    {
                        oneCoinContainer.AddCoin(oneCoin);
                        if (oneCoinContainer.GetMaxPercentFilling())
                        {
                            AlertCoinMaximun(oneCoinValue);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Löscht die Münzen aus der Variable<para name="insertCoins"/> 
        /// </summary>
        public void NotAcceptValueInput()
        {
            insertCoins.Clear();
            insertValue = 0;
        }

        /// <summary>
        /// Gibt das Rückgeld dem Kunden heraus
        /// </summary>
        /// <param name="outputValue">Betrag, welcher ausbezahlt werden muss</param>
        /// <returns>Die Anzahl von den verschiedenen Münzen, welche ausgegeben werden müssen</returns>
        public QuantityCoins GetChange(int outputValue)
        {
            var outputCoins = new QuantityCoins();
            while (outputValue >= 100000)
            {
                if (containers[12].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(100000);
                    containers[12].RemoveCoin();
                    if (containers[12].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(100000);
                    }
                    outputValue -= 100000;
                }
                else
                {
                    break;
                }
            } 
            while (outputValue >= 20000)
            {
                if (containers[11].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(20000);
                    containers[11].RemoveCoin();
                    if (containers[11].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(20000);
                    }
                    outputValue -= 20000;
                }
                else
                {
                    break;
                }
            } 
            while (outputValue >= 10000)
            {
                if (containers[10].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(10000);
                    containers[10].RemoveCoin();
                    if (containers[10].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(10000);
                    }
                    outputValue -= 10000;
                }
                else
                {
                    break;
                }
            } 
            while (outputValue >= 5000)
            {
                if (containers[9].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(5000);
                    containers[9].RemoveCoin();
                    if (containers[9].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(5000);
                    }
                    outputValue -= 5000;
                }
                else
                {
                    break;
                }
            } 
            while (outputValue >= 2000)
            {
                if (containers[8].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(2000);
                    containers[8].RemoveCoin();
                    if (containers[8].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(2000);
                    }
                    outputValue -= 2000;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 1000)
            {
                if (containers[7].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(1000);
                    containers[7].RemoveCoin();
                    if (containers[7].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(1000);
                    }
                    outputValue -= 1000;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 500)
            {
                if (containers[6].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(500);
                    containers[6].RemoveCoin();
                    if (containers[6].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(500);
                    }
                    outputValue -= 500;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 200)
            {
                if (containers[5].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(200);
                    containers[5].RemoveCoin();
                    if (containers[5].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(200);
                    }
                    outputValue -= 200;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 100)
            {
                if (containers[4].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(100);
                    containers[4].RemoveCoin();
                    if (containers[4].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(100);
                    }
                    outputValue -= 100;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 50)
            {
                if (containers[3].CountCoins() >= 1)
                {
                    if (outputValue < 100)
                    {
                        var isOddNumber = false;
                        try
                        {
                            var dividedOutput = outputValue / 2;
                        }
                        catch (Exception)
                        {
                            isOddNumber = true;
                        }
                        if (!isOddNumber)
                        {
                            if (containers[0].CountCoins() <= 0)
                            {
                                break;
                            }
                        }
                    }
                    outputCoins.AddCoinValue(50);
                    containers[3].RemoveCoin();
                    if (containers[3].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(50);
                    }
                    outputValue -= 50;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 20)
            {
                if (containers[2].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(20);
                    if (containers[2].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(20);
                    }
                    containers[2].RemoveCoin();
                    outputValue -= 20;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 10)
            {
                if (containers[1].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(10);
                    containers[1].RemoveCoin();
                    if (containers[1].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(10);
                    }
                    outputValue -= 10;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 5)
            {
                if (containers[0].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(5);
                    containers[0].RemoveCoin();
                    if (containers[0].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(5);
                    }
                    outputValue -= 5;
                }
                else
                {
                    throw new Exception("Es kann leider kein Rückgeld gegeben werden, da die entsprechenden Münzen nicht vorhanden sind. Geben Sie Abbruch ein und Sie erhalten ihr Geld zurück");
                }
            }
            insertCoins = new List<Coin>();
            insertValue = 0;
            return outputCoins;
        }

        /// <summary>
        /// Gibt den totalen Geldwert von allen Münzen in der Kasse zurück.
        /// </summary>
        /// <returns>Totalen Geldwert von allen Münzen in der Kasse</returns>
        public int GetValueTotal()
        {
            var totalValue = 0;
            foreach (var oneContainer in containers)
            {
                totalValue += oneContainer.GetCoinsValue() * oneContainer.CountCoins();
            }
            return totalValue;
        }

        /// <summary>
        /// Fragt die effektive Anzahl von Münzen im Automaten ab.
        /// </summary>
        /// <returns></returns>
        public QuantityCoins GetQuantityCoins()
        {
            var quantityCoins = new QuantityCoins();
            foreach (var oneContainer in containers)
            {
                for (int i = 1; i < oneContainer.CountCoins(); i++)
                {
                    quantityCoins.AddCoinValue(oneContainer.GetCoinsValue());
                }
            }
            return quantityCoins;
        }

        /// <summary>
        /// Teilt dem Benutzer mit, dass ein CoinContainer den minimum Füllungsgrad unterschritten hat.
        /// </summary>
        /// <param name="coin">Münzwert vom unterschrittenen CoinContainer</param>
        private void AlertCoinMinimun(int coin)
        {
            MessageService.AddWarningMessage("Minimaler Füllungsgrad unterschritten", $"Der CoinContainer von der Münze {coin} hat den minimalen Füllungsgrad unterschritten");
        }

        /// <summary>
        /// Teilt dem Benutzer mit, dass ein CoinContainer den maximalen Füllungsgrad überschritten hat.
        /// </summary>
        /// <param name="coin">Münzwert vom überschritenen CoinContainer</param>
        private void AlertCoinMaximun(int coin)
        {
            MessageService.AddWarningMessage("Maximaler Füllungsgrad überschritten", $"Der CoinContainer von der Münzen {coin} hat den maximalen Füllungsgrad überschritten");
        }

        /// <summary>
        /// Rundet einen Wert, dass es ein gültiger Frankenbetrag ist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public decimal Round(decimal value)
        {
            decimal roundedValue = Math.Round(value, 2);
            decimal lastNumber = decimal.Parse(roundedValue.ToString().ToCharArray().Last().ToString());

            if (lastNumber > 5)
                roundedValue = roundedValue + (10 - lastNumber) / 100;
            else if (lastNumber < 5)
                roundedValue = roundedValue - lastNumber / 100;

            return roundedValue;

        }

        /// <summary>
        /// Erstellt eine Rechnung
        /// </summary>
        /// <param name="gasPump"></param>
        /// <returns></returns>
        public Receipt CreateReceipt(GasPump gasPump)
        {
            Receipt receipt = new Receipt();
            receipt.RelatedFuel = gasPump.ActiveTap.Fuel;
            receipt.RelatedLiter = float.Parse(gasPump.Liter.ToString());
            receipt.Sum = gasPump.ToPayValue;
            DateTime datum = DateTime.Now;
            CultureInfo german = new CultureInfo("de");
            receipt.Date = DateTime.Parse(DateTime.Parse(datum.ToString()).ToString("yyyy/MM/dd HH:mm:ss"));
            GasStation.GetInstance().AddReceipt(receipt);
            return receipt;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.GUI;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    public class CashRegister : ICashRegister
    {
        /// <summary>
        /// Liste von allen Zapfsäulen
        /// </summary>
        public List<GasPump> GasPumpList { get; set; }
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
        private CoinContainer[] containers = new CoinContainer[12];
        /// <summary>
        /// Der insgesamte Wert von den Münzen, welche eingeworfen worden sind.
        /// </summary>
        private int insertValue = 0;

        public CashRegister()
        {
            containers[0] = new CoinContainer(10, 100);
            containers[1] = new CoinContainer(20, 100);
            containers[2] = new CoinContainer(50, 100);
            containers[3] = new CoinContainer(100, 100);
            containers[4] = new CoinContainer(200, 100);
            containers[5] = new CoinContainer(500, 100);
            containers[6] = new CoinContainer(1000, 100);
            containers[7] = new CoinContainer(2000, 100);
            containers[8] = new CoinContainer(5000, 100);
            containers[9] = new CoinContainer(10000, 100);
            containers[10] = new CoinContainer(20000, 100);
            containers[11] = new CoinContainer(100000, 100);
        }

        /// <summary>
        /// Öffnet das Fenster von der Kasse
        /// </summary>
        public void ShowDisplay()
        {
            CashRegisterDisplay display = new CashRegisterDisplay(this);
            display.Show();
        }

        /// <summary>
        /// Schliesst die Zahlung ab, sofern die Schulden beglichen wurden
        /// </summary>
        /// <param name="gasPump"></param>
        /// <param name="verifyToPayValue"></param>
        /// <returns></returns>
        public bool FinishPayment(GasPump gasPump, bool verifyToPayValue = true)
        {
            if (verifyToPayValue)
            {
                if (gasPump.ToPayValue > 0)
                {
                    return false;
                }
            }
            GasPumpList.Find(g => g == gasPump).Refresh();
            return true;
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
            while (outputValue >= 500)
            {
                if (containers[5].CountCoins() > 0)
                {
                    outputCoins.AddCoinValue(500);
                    containers[5].RemoveCoin();
                    if (containers[5].GetMinPercentFlling())
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
                if (containers[4].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(200);
                    containers[4].RemoveCoin();
                    if (containers[4].GetMinPercentFlling())
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
                if (containers[3].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(100);
                    containers[3].RemoveCoin();
                    if (containers[3].GetMinPercentFlling())
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
                if (containers[2].CountCoins() >= 1)
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
                    containers[2].RemoveCoin();
                    if (containers[2].GetMinPercentFlling())
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
                if (containers[1].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(20);
                    if (containers[1].GetMinPercentFlling())
                    {
                        AlertCoinMinimun(20);
                    }
                    containers[1].RemoveCoin();
                    outputValue -= 20;
                }
                else
                {
                    break;
                }
            }
            while (outputValue >= 10)
            {
                if (containers[0].CountCoins() >= 1)
                {
                    outputCoins.AddCoinValue(10);
                    containers[0].RemoveCoin();
                    if (containers[0].GetMinPercentFlling())
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
        /// Gibt an wieviele effektive Münzen schon in den Automaten geworfen wurden.
        /// </summary>
        /// <returns>Anzahl der Münzen</returns>
        public List<Coin> GetInsertCoins()
        {
            return insertCoins;
        }

        /// <summary>
        /// Gibt den totalen Geldwert von allen Münzen im Apparat zurück.
        /// </summary>
        /// <returns>Totalen Geldwert von allen Münzen im Apparat</returns>
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
        /// Überprpüft ob der Maximalewert an Münzen im Automat überschritten wurde und wenn ja wird dies mitgeteilt.
        /// </summary>
        /// <param name="valueToal">Anzahl der Münzen, welche im Automat vorhanden sind</param>
        public void AlertValueTotalMaximum(int valueTotal)
        {
            if (valueTotal > maxCoins)
            {
                Console.WriteLine("Der Maximalwert von den Münzen, welche im Automat sein können, wurde überschritten");
            }
        }

        /// <summary>
        /// Prozentueller Füllungsgrad von einem Behälter abfragen
        /// </summary>
        public double GetPercentCoin(int coin)
        {
            foreach (var oneContainer in containers)
            {
                if (oneContainer.GetCoinsValue() == coin)
                {
                    return oneContainer.GetPercentCoin();
                }
            }
            return 101;
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
            Console.WriteLine($"Der CoinContainer von der Münze {coin} hat den minimalen Füllungsgrad unterschritten");
        }

        /// <summary>
        /// Teilt dem Benutzer mit, dass ein CoinContainer den maximalen Füllungsgrad überschritten hat.
        /// </summary>
        /// <param name="coin">Münzwert vom überschritenen CoinContainer</param>
        private void AlertCoinMaximun(int coin)
        {
            Console.WriteLine($"Der CoinContainer von der Münzen {coin} hat den maximalen Füllungsgrad überschritten");
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
                  
            if(lastNumber > 5)
                roundedValue = roundedValue + (10 - lastNumber)/100;
            else if(lastNumber < 5)
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
            receipt.Date = DateTime.Today;
            return receipt;
        }
    }
}

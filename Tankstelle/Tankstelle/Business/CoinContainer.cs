using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Business.TankService;

namespace Tankstelle.Business
{
    public class CoinContainer
    {
        #region private Felder
        /// <summary>
        /// Der maximale Füllungsgrad, welcher der CoinContainer haben darf.
        /// </summary>
        private readonly float _maxPercentFilling = 90;
        /// <summary>
        /// Der minimale Füllungsgrad welcher der CoinContainer haben darf.
        /// </summary>
        private readonly float _minPercentFilling = 10;
        /// <summary>
        /// Die Coins, welche sich im CoinContainer befinden
        /// </summary>
        private Coin[] _coins = new Coin[200];
        /// <summary>
        /// Werte von den Münzen/Noten, welche sich im CoinContainer befinden
        /// </summary>
        private int _coinsValue;
        /// <summary>
        /// Der prozentualle Füllungsgrad
        /// </summary>
        private double _percentFilling;
        /// <summary>
        /// Maximale Anzahl Coins, welche in der Kasse sein können.
        /// </summary>
        private int _maximunCoins;
        #endregion

        #region Konstruktor
        public CoinContainer(int coinValue, int maximunCoins)
        {
            _coinsValue = coinValue;
            _maximunCoins = maximunCoins;
            Coin[] coins = GasStation.GetInstance().GetCoins().Where(c => c.GetValue() == coinValue).ToArray();

            for (int i = 0; i < coins.Count(); i++)
            {
                try
                {
                    _coins[i] = coins[i];
                }
                catch(IndexOutOfRangeException ex)
                {
                   
                }
            }

            //for (int i = 0; i < 20; i++)
            //{
            //    _coins[i] = new Coin(_coinsValue);
            //}

            _percentFilling = 100.0 / _maximunCoins * _coins.Where(x => x != null).Count();
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Gibt den Wert von den Coins zurück, welche sich in diesem CoinContainer befinden.
        /// </summary>
        /// <returns></returns>
        public int GetCoinsValue()
        {
            return _coinsValue;
        }

        /// <summary>
        /// Fügt dem Coin Container eine Münze hinzu
        /// </summary>
        /// <param name="coin">Münze welche dem CoinContainer hinzugefügt werden soll</param>
        public void AddCoin(Coin coin)
        {
            for (int i = 0; i < 200; i++)
            {
                if (_coins[i] == null)
                {
                    try
                    {
                        _coins[i] = coin;
                        break;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        MessageService.AddWarningMessage("Zu viele Münzen/Noten", $"Das Limit für die Noten/Münzen {coin.GetValue()} wurde erreicht. Es können keine weiteren Geldstücken mit diesem Wert eingeworfen werden. Das Geldstück mit dem Wert {coin.GetValue()} wird nicht in der Kasse gespeichert.");
                    }
                }
            }
            _percentFilling = 100.0 / _maximunCoins * _coins.Where(x => x != null).Count();
        }

        /// <summary>
        /// Entfernt eine Münze von diesem CoinContainer
        /// </summary>
        public void RemoveCoin()
        {
            for (int i = 0; i < 200; i++)
            {
                if (_coins[i] == null)
                {
                    _coins[i - 1] = null;
                    break;
                }
            }
        }

        /// <summary>
        /// Zählt wieviele Münzen in diesem CoinContainer vorhanden sind
        /// </summary>
        /// <returns></returns>
        public int CountCoins()
        {
            var counter = 0;
            for (int i = 0; i < _coins.Count(); i++)
            {
                if (_coins[i] != null)
                    counter++;
                else
                    break;
            }
            return counter;
        }

        /// <summary>
        /// Gibt zurück ob der minimale Füllungsgrad unterschritten wurde oder nicht.
        /// </summary>
        /// <returns></returns>
        public bool GetMinPercentFlling()
        {
            if (_percentFilling < _minPercentFilling)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gibt zurück ob der maximale Füllungsgrad überschritten wurde oder nicht.
        /// </summary>
        /// <returns></returns>
        public bool GetMaxPercentFilling()
        {
            if (_percentFilling > _maxPercentFilling)
            {
                return true;
            }
            return false;
        }

        public Coin[] GetCoins()
        {
            return _coins;
        }

        /// <summary>
        /// Gibt zurück zu wieviel Prozent der CoinContainer gefüllt ist
        /// </summary>
        /// <returns>Der Prozentwert</returns>
        public double GetPercentCoin()
        {
            return _percentFilling;
        }
        #endregion
    }
}

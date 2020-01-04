using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class CoinContainer
    {
        private readonly float _maxPercentFilling = 90;
        private readonly float _minPercentFilling = 10;
        private Coin[] _coins = new Coin[200];
        private int _coinsValue;
        private double _percentFilling;
        private int _maximunCoins;

        public CoinContainer(int coinValue, int maximunCoins)
        {
            _coinsValue = coinValue;
            _maximunCoins = maximunCoins;
            for (int i = 0; i < 50; i++)
            {
                _coins[i] = new Coin(_coinsValue);
            }

            _percentFilling = 100.0 / _maximunCoins * _coins.Where(x => x != null).Count();
        }

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
                    _coins[i] = coin;
                    break;
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

        /// <summary>
        /// Gibt den insgesamten Wert von den Münzen in diesem Container zurück
        /// </summary>
        public int GetTotalCoinsValue()
        {
            return _coins.Count() * _coinsValue;
        }

        /// <summary>
        /// Gibt zurück zu wieviel Prozent der CoinContainer gefüllt ist
        /// </summary>
        /// <returns>Der Prozentwert</returns>
        public double GetPercentCoin()
        {
            return _percentFilling;
        }
    }
}

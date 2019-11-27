using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public struct QuantityCoins
    {
        private int _numberOf5Coins;
        private int _numberOf10Coins;
        private int _numberOf20Coins;
        private int _numberOf50Coins;
        private int _numberOf100Coins;
        private int _numberOf200Coins;
        private int _numberOf500Coins;
        private int _numberOf1000Coins;
        private int _numberOf2000Coins;
        private int _numberOf5000Coins;
        private int _numberOf10000Coins;
        private int _numberOf20000Coins;
        private int _numberOf100000Coins;
        public int[] CountCoins()
        {
            var numberOfAllCoins = new int[13];
            numberOfAllCoins[0] = _numberOf5Coins;
            numberOfAllCoins[1] = _numberOf10Coins;
            numberOfAllCoins[2] = _numberOf20Coins;
            numberOfAllCoins[3] = _numberOf50Coins;
            numberOfAllCoins[4] = _numberOf100Coins;
            numberOfAllCoins[5] = _numberOf200Coins;
            numberOfAllCoins[6] = _numberOf500Coins;
            numberOfAllCoins[7] = _numberOf1000Coins;
            numberOfAllCoins[8] = _numberOf2000Coins;
            numberOfAllCoins[9] = _numberOf5000Coins;
            numberOfAllCoins[10] = _numberOf10000Coins;
            numberOfAllCoins[11] = _numberOf20000Coins;
            numberOfAllCoins[12] = _numberOf100000Coins;
            return numberOfAllCoins;
        }
        public void AddCoinValue(int value)
        {
            switch (value)
            {
                case 5:
                    _numberOf5Coins++;
                    break;
                case 10:
                    _numberOf10Coins++;
                    break;
                case 20:
                    _numberOf20Coins++;
                    break;
                case 50:
                    _numberOf50Coins++;
                    break;
                case 100:
                    _numberOf100Coins++;
                    break;
                case 200:
                    _numberOf200Coins++;
                    break;
                case 500:
                    _numberOf500Coins++;
                    break;
                case 1000:
                    _numberOf1000Coins++;
                    break;
                case 2000:
                    _numberOf2000Coins++;
                    break;
                case 5000:
                    _numberOf5000Coins++;
                    break;
                case 10000:
                    _numberOf10000Coins++;
                    break;
                case 20000:
                    _numberOf20000Coins++;
                    break;
                case 100000:
                    _numberOf100000Coins++;
                    break;
                default:
                    break;
            }
        }
    }
}

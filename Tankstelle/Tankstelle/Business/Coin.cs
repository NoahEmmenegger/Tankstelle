using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankstelle.Business
{
    public class Coin
    {
        private int _value;
        private string _currency;

        public Coin(int value, string currency)
        {
            _value = value;
            _currency = currency;
        }

        public int GetValue()
        {
            return _value;
        }
    }
}

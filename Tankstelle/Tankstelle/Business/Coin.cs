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

        public Coin(int value)
        {
            _value = value;
        }

        public int GetValue()
        {
            return _value;
        }
    }
}

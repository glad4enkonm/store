using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public class Order
    {
        public long Id;
        public Dictionary<long, int> QuantityByItemId;
    }
}
    
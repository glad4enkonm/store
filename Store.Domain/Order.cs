using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain
{
    public class Order
    {
        public UInt64 Id;
        public Dictionary<UInt64, UInt32> QuantityByItemId;
    }
}

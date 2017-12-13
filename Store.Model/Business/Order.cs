using System.Collections.Generic;

namespace Store.Model.Business
{
    public class Order
    {
        public long Id;
        public Dictionary<long, int> QuantityByItemId;
    }
}
    
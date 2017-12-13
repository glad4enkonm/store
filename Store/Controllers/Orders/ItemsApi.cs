using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers.Orders
{
    public class ItemsApi : Abstract.Orders.ItemsApi
    { 

        public override async Task DeleteAsync([FromRoute]long? orderId, [FromRoute]long? itemId)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> PatchAsync([FromRoute]long? orderId, [FromRoute]long? itemId, [FromBody]int? quantity)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> PutAsync([FromRoute]long? orderId, [FromRoute]long? itemId, [FromBody]int? quantity)
        {
            throw new NotImplementedException();
        }
    }
}

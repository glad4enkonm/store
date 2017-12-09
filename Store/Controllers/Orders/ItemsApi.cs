using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;

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

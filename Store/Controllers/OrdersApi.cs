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

namespace Store.Controllers
{
    public class OrdersApi : Abstract.OrdersApi
    { 

        public override async Task DeleteAsync([FromRoute]long? orderId)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> GetAsync()
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> GetAsync([FromRoute]long? orderId)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> PatchAsync([FromRoute]long? orderId, [FromBody]QuantityList quantityList)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> PutAsync()
        {
            throw new NotImplementedException();
        }
    }
}

/*
 * Store
 *
 * This is a excersise API for managing users, orders and items.
 *
 * OpenAPI spec version: 0.1.0
 * Contact: glad4enkonm@gmail.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers.Abstract.Orders
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ItemsApi : Controller
    { 

        /// <summary>
        /// Delete item from an arder.
        /// </summary>
        
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <response code="200">Ok.</response>
        /// <response code="400">Invalid id specified.</response>
        [HttpDelete]
        [Route("/orders/{orderId}/items/{itemId}")]
        public abstract Task DeleteAsync([FromRoute]long? orderId, [FromRoute]long? itemId);


        /// <summary>
        /// Modify items quantity in an arder.
        /// </summary>
        
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <param name="quantity"></param>
        /// <response code="200">Ok.</response>
        /// <response code="400">Invalid id specified.</response>
        [HttpPatch]
        [Route("/orders/{orderId}/items/{itemId}")]
        public abstract Task<IActionResult> PatchAsync([FromRoute]long? orderId, [FromRoute]long? itemId, [FromBody]int? quantity);


        /// <summary>
        /// Add some items to an order.
        /// </summary>
        
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <param name="quantity"></param>
        /// <response code="200">Ok.</response>
        /// <response code="400">Invalid id specified.</response>
        [HttpPut]
        [Route("/orders/{orderId}/items/{itemId}")]
        public abstract Task<IActionResult> PutAsync([FromRoute]long? orderId, [FromRoute]long? itemId, [FromBody]int? quantity);
    }
}

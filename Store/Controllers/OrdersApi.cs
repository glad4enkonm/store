using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;
using Store.Domain.Repositories;
using Store.Domain.Exceptions;
using System.Collections.Generic;

namespace Store.Controllers
{
    public class OrdersApi : Abstract.OrdersApi
    {
        IOrderRepository _orderRepository;

        public override async Task DeleteAsync([FromRoute]long? orderId)
        {
            throw new NotImplementedException();
        }

        public OrdersApi(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<IActionResult> GetAsync()
        {
            IEnumerable<Domain.Order> orders = await _orderRepository.List();
            return Json(Models.Mapping.Instance.Map<OrderList>(orders));
        }


        public override async Task<IActionResult> GetAsync([FromRoute]long? orderId)
        {
            if (!orderId.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var order = await _orderRepository.GetById(orderId.Value);
                return Json(Mapping.Instance.Map<Models.Order>(order));
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
        }


        public override async Task<IActionResult> PatchAsync([FromRoute]long? orderId, [FromBody]QuantityList quantityList)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> PutAsync()
        {
            Domain.Order order = await _orderRepository.Create();
            return Json(order.Id);
        }
    }
}

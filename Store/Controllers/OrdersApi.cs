using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business = Store.Model.Business;
using Transport = Store.Model.Transport;
using System.Collections.Generic;
using Store.Model.Business.Repositories.Exceptions;
using Store.Model.Business.Repositories;
using Store.Model;

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
            IEnumerable<Business.Order> orders = await _orderRepository.List();
            return Json(Mapping.Instance.Map<Transport.OrderList>(orders));
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
                return Json(Mapping.Instance.Map<Transport.Order>(order));
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
        }


        public override async Task<IActionResult> PatchAsync([FromRoute]long? orderId, [FromBody]Transport.QuantityList quantityList)
        {
            throw new NotImplementedException();
        }


        public override async Task<IActionResult> PutAsync()
        {
            Business.Order order = await _orderRepository.Create();
            return Json(order.Id);
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Model.Business.Repositories;
using Business = Store.Model.Business;
using Transport = Store.Model.Transport;
using Store.Model.Business.Repositories.Exceptions;
using Store.Model;
using System.Net;

namespace Store.Controllers.Orders
{
    public class ItemsApi : Abstract.Orders.ItemsApi
    {
        IOrderRepository _orderRepository;
        IItemRepository _itemRepository;
        public ItemsApi(IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        public override async Task<IActionResult> DeleteAsync([FromRoute]long? orderId, [FromRoute]long? itemId)
        {
            if (!orderId.HasValue || !itemId.HasValue)
            {
                return BadRequest();
            }

            try
            {
                //validation
                var item = await _itemRepository.GetById(itemId.Value);
                var order = await _orderRepository.GetById(orderId.Value);
                
                if (order.QuantityByItemId.ContainsKey(itemId.Value))
                    order.QuantityByItemId.Remove(itemId.Value);

                var orderUpdated = await _orderRepository.UpdateOrder(order);
                return Ok();
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

        }


        public override async Task<IActionResult> PatchAsync([FromRoute]long? orderId, [FromRoute]long? itemId, [FromBody]int? quantity)
        {
            if (!orderId.HasValue || !itemId.HasValue || !quantity.HasValue || quantity.Value < 0)
            {
                return BadRequest();
            }

            try
            {
                //validation
                var item = await _itemRepository.GetById(itemId.Value);
                var order = await _orderRepository.GetById(orderId.Value);

                order.QuantityByItemId[item.Id] = quantity.Value;

                var orderUpdated = await _orderRepository.UpdateOrder(order);
                return Json(Mapping.Instance.Map<Transport.Order>(order));
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }


        public override async Task<IActionResult> PutAsync([FromRoute]long? orderId, [FromRoute]long? itemId, [FromBody]int? quantity)
        {
            if (!orderId.HasValue || !itemId.HasValue || !quantity.HasValue || quantity.Value < 0)
            {
                return BadRequest();
            }            

            try
            {
                //validation
                var item = await _itemRepository.GetById(itemId.Value);
                var order = await _orderRepository.GetById(orderId.Value);

                int before = 0;
                if (order.QuantityByItemId.ContainsKey(item.Id))
                {
                    before = order.QuantityByItemId[item.Id];
                }
                order.QuantityByItemId[item.Id] = before + quantity.Value;

                var orderUpdated = await _orderRepository.UpdateOrder(order);
                return Json(Mapping.Instance.Map<Transport.Order>(order));
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

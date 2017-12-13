using AutoMapper;
using Store.Model.Business.Repositories.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Model.Business.Repositories.InMemory
{
    public class OrderRepository: IOrderRepository
    {
        private ConcurrentDictionary<long, ConcurrentOrder> _orders = new ConcurrentDictionary<long, ConcurrentOrder>();

        Random _random = new Random(DateTime.Now.Millisecond);        

        IMapper _mapper = (new MapperConfiguration(conf =>
        {
            conf.CreateMap<Order, ConcurrentOrder>()
                .ForMember(dst => dst.QuantityByItemId,
                    opt => opt.MapFrom(src => new ConcurrentDictionary<long, int>(src.QuantityByItemId)));

            conf.CreateMap<ConcurrentOrder, Order>()
                .ForMember(dst => dst.QuantityByItemId,
                    opt => opt.MapFrom(src =>
                        src.QuantityByItemId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
        })).CreateMapper();

        class ConcurrentOrder
        {
            public long Id;
            public ConcurrentDictionary<long, int> QuantityByItemId;
        }

        public Task<Order> Create()
        {
            long newId = _random.Next(1, Int32.MaxValue); // get some new id
            var newOrder = new Order { Id = newId, QuantityByItemId = null };

            var concurrentOrder = 
                _orders.GetOrAdd(newId, _mapper.Map<ConcurrentOrder>(newOrder));

            return Task.FromResult(newOrder);
        }

        public Task<Order> GetById(long id)
        {
            if (!_orders.TryGetValue(id, out ConcurrentOrder concurrentOrder))
            {
                throw new OrderNotFoundException();
            }
            return Task.FromResult(_mapper.Map<Order>(concurrentOrder));
        }

        public Task<Order> UpdateOrder(Order order)
        {
            if (!_orders.TryGetValue(order.Id, out ConcurrentOrder concurrentOrder))
            {
                throw new OrderNotFoundException();
            }

            // TO DO: check item exist

            concurrentOrder.QuantityByItemId = 
                new ConcurrentDictionary<long, int>(order.QuantityByItemId);

            return Task.FromResult(_mapper.Map<Order>(concurrentOrder));
        }

        public Task<IEnumerable<Order>> List()
        {
            var orders = _orders.Values.ToList();
            return Task.FromResult(_mapper.Map<IEnumerable<Order>>(orders));
        }

        public Task DeleteById(long id)
        {
            if (!_orders.TryRemove(id, out ConcurrentOrder order))
            {
                throw new OrderNotFoundException();
            }
            return Task.CompletedTask;
        }

        public Task<Order> AddItem(long OrderId, long itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> RemoveItem(long OrderId, long itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateQuantity(long OrderId, long itemId, ushort quantity)
        {
            throw new NotImplementedException();
        }
    }
}

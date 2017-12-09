using AutoMapper;
using Store.Domain.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories.InMemory
{
    public class OrderRepository: IOrderRepository
    {
        private ConcurrentDictionary<long, ConcurrentOrder> _orders = new ConcurrentDictionary<long, ConcurrentOrder>();

        Random _random = new Random(DateTime.Now.Millisecond);        

        IMapper _mapper = (new MapperConfiguration(conf =>
        {
            conf.CreateMap<Domain.Order, ConcurrentOrder>()
                .ForMember(dst => dst.QuantityByItemId,
                    opt => opt.MapFrom(src => new ConcurrentDictionary<long, int>(src.QuantityByItemId)));

            conf.CreateMap<ConcurrentOrder, Domain.Order>()
                .ForMember(dst => dst.QuantityByItemId,
                    opt => opt.MapFrom(src =>
                        src.QuantityByItemId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
        })).CreateMapper();

        class ConcurrentOrder
        {
            public long Id;
            public ConcurrentDictionary<long, int> QuantityByItemId;
        }

        public Task<Domain.Order> Create()
        {
            long newId = _random.Next(1, Int32.MaxValue); // get some new id
            var newOrder = new Domain.Order { Id = newId, QuantityByItemId = null };

            var concurrentOrder = 
                _orders.GetOrAdd(newId, _mapper.Map<ConcurrentOrder>(newOrder));

            return Task.FromResult(newOrder);
        }

        public Task<Domain.Order> GetById(long id)
        {
            if (!_orders.TryGetValue(id, out ConcurrentOrder concurrentOrder))
            {
                throw new OrderNotFoundException();
            }
            return Task.FromResult(_mapper.Map<Domain.Order>(concurrentOrder));
        }

        public Task<IEnumerable<Domain.Order>> List()
        {
            var orders = _orders.Values.ToList();
            return Task.FromResult(_mapper.Map<IEnumerable<Domain.Order>>(orders));
        }

        public Task<Order> DeleteById(long id)
        {
            throw new NotImplementedException();
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Create();
        Task<Order> GetById(UInt64 id);
        Task<Order> DeleteById(UInt64 id);

        Task<IEnumerable<Order>> List(UInt64 OrderId);
        Task<Order> AddItem(UInt64 OrderId, UInt64 itemId);
        Task<Order> RemoveItem(UInt64 OrderId, UInt64 itemId);

        Task<Order> UpdateQuantity(UInt64 OrderId, UInt64 itemId, ushort quantity);
    }
}

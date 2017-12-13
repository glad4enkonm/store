using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Model.Business.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Create();
        Task<Order> GetById(long id);
        Task<Order> DeleteById(long id);

        Task<IEnumerable<Order>> List();
        Task<Order> AddItem(long OrderId, long itemId);
        Task<Order> RemoveItem(long OrderId, long itemId);

        Task<Order> UpdateQuantity(long OrderId, long itemId, ushort quantity);
    }
}

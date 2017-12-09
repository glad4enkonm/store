using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetById(UInt64 id);
        Task<IEnumerable<Item>> List();
    }
}

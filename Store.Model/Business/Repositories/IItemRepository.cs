using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model.Business.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetById(long id);
        Task<IEnumerable<Item>> List();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Model.Business.Repositories;
using Business = Store.Model.Business;
using Transport = Store.Model.Transport;
using Store.Model.Business.Repositories.Exceptions;
using Store.Model;

namespace Store.Controllers
{
    public class ItemsApi : Abstract.ItemsApi
    {
        IItemRepository _repository;       

        public ItemsApi(IItemRepository repository)
        {
            _repository = repository;
        }

        public override async Task<IActionResult> GetAsync()
        {
            IEnumerable<Business.Item > list = await _repository.List();
            return Json(Mapping.Instance.Map<Transport.ItemList>(list));
        }


        public override async Task<IActionResult> GetAsync([FromRoute]long? itemId)
        {
            if (!itemId.HasValue)
            {
                return BadRequest();
            }

            try
            {
                Business.Item item = await _repository.GetById(itemId.Value);
                return Json(Mapping.Instance.Map<Transport.Item>(item));
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}

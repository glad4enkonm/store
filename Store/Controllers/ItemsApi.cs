using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Repositories;
using Store.Domain.Exceptions;
using Store.Models;

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
            IEnumerable<Domain.Item> list = await _repository.List();
            return Json(Mapping.Instance.Map<Models.ItemList>(list));
        }


        public override async Task<IActionResult> GetAsync([FromRoute]long? itemId)
        {
            if (!itemId.HasValue)
            {
                return BadRequest();
            }

            try
            {
                Domain.Item item = await _repository.GetById(itemId.Value);
                return Json(Mapping.Instance.Map<Models.Item>(item));
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}

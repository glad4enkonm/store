using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Models;
using Microsoft.AspNetCore.JsonPatch;
using Store.Domain.Repositories;
using AutoMapper;
using Store.Domain.Exceptions;

namespace Store.Controllers
{
    public class ItemsApi : Abstract.ItemsApi
    {
        IItemRepository _repository;

        static ItemsApi()
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<Domain.Item, Models.Item>()
                    .ForMember(dst => dst.ItemId, opt => opt.MapFrom(src => src.Id)));
        }

        public ItemsApi(IItemRepository repository)
        {
            _repository = repository;
        }

        public override async Task<IActionResult> GetAsync()
        {
            IEnumerable<Domain.Item> list = await _repository.List();
            return Json(Mapper.Map<Models.ItemList>(list));
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
                return Json(Mapper.Map<Models.Item>(item));
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}

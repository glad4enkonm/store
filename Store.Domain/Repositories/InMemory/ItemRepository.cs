﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories.InMemory
{
    public class ItemRepository : IItemRepository
    {
        List<Item> _items = new List<Item>()
        {
            new Item() { Id = 1, Description = "Store A", Price = 10},
            new Item() { Id = 2, Description = "Store B", Price = 20},
            new Item() { Id = 3, Description = "Store C", Price = 30},
            new Item() { Id = 4, Description = "Store D", Price = 40},
            new Item() { Id = 5, Description = "Store E", Price = 50},            
        };


        public Task<Item> GetById(UInt64 id)
        {
            var item = _items.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new Exception();//ItemNotFoundException();
            }

            return Task.FromResult(item);
        }


        public Task<IEnumerable<Item>> List() => Task.FromResult<IEnumerable<Item>>(_items);
    }
}

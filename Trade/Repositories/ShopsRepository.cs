using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Trade.Models;

namespace Trade.Repositories
{
    public class ShopsRepository
    {
        private List<Shop> _shop;

        public ShopsRepository()
        {
            _shop = new List<Shop>();
        }
        public List<Shop> GetAll()
        {
            return _shop;
        }

        public void Delete(Shop shop)
        {
            _shop.Remove(shop);
            Save();
        }

        public void Insert(Shop shop)
        {
            _shop.Add(shop);
            Save();
        }

        public void Save()
        {

        }
    }
}

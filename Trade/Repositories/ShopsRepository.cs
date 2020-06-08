using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Trade.Models;

namespace Trade.Repositories
{
    public class ShopsRepository : IShopsRepository
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

        public void ShowProductsInEachShop(List<Product> products)
        {
            var gruppedByShops = new Dictionary<string, string>();
            foreach (var product in products)
            {
                foreach (var shop in product.Shops[0].ListShopName)
                {
                    if (!gruppedByShops.ContainsKey(shop))
                    {
                        gruppedByShops.Add(shop, product.Name);
                    }
                    else
                    {
                        gruppedByShops[shop] += ", " + product.Name;
                    }
                }
            }
            foreach (var element in gruppedByShops)
            {
                Console.WriteLine("In " + element.Key + " is in stock:");
                Console.WriteLine(element.Value);
                Console.WriteLine("______________________________________");
            }
        }
    }
}

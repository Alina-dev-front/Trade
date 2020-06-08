using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trade.Repositories;

namespace Trade.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public List<string> ListShopName { get; set; }
        public List<Product> ProductList { get; set; }

        public Shop(string shops)
        {
            ListShopName = ConvertStringToList(shops);
        }


        public List<string> ConvertStringToList(string shops)
        {
            return shops.Split(",").ToList();
        }
    }
}


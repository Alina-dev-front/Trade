using System;
using System.Collections.Generic;
using System.Text;

namespace Trade
{
    class Shop
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }

        public Shop(int shopId, string shopName)
        {
            ShopId = shopId;
            ShopName = shopName;
        }
    }
}

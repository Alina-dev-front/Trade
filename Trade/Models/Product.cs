using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvHelper.Configuration.Attributes;
using Trade.Repositories;


namespace Trade.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Producer Producer { get; set; }
        public List<Shop> Shops { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Price: {Price} Producer: {Producer.ProducerName} Shop: {Shops[0].ListShopName}";
        }

        public Product(string name, int price, Producer producer)
        {
            Name = name;
            Price = price;
            Producer = producer;
            Shops = new List<Shop>();
        }

        public Product()
        {
        }
    }
}

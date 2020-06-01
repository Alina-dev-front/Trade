using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;
using Trade.Repositories;


namespace Trade.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Producer Producer { get; set; }
        public Shop Shop { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Price: {Price} Producer: {Producer} Shop: {Shop}";
        }

        public Product(string name, int price, Producer producer, Shop shop)
        {
            Name = name;
            Price = price;
            Producer = producer;
            Shop = shop;
        }

        public Product()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;


namespace Trade
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Producer Producer { get; set; }
        public Shop Shop { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Price: {Price} Producer: {Producer} Shop: {Shop}";
        }

        public Product(int id, string name, int price, Producer producer, Shop shop)
        {
            Id = id;
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

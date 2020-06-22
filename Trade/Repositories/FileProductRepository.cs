using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trade.Models;
using System.Text.Json;

namespace Trade.Repositories
{
    public class FileProductRepository : IProductRepository
    {
        private List<Product> _products;
        private string _csvPath;
        private string _jsonPath;

        public FileProductRepository()
        {
            _csvPath = new FileHandler().GetUserFileDirectory();
            _products = GetProducts();
            _jsonPath = new FileHandler().GetUserJsonPath();
        }

        public List<Product> GetProducts() 
        {
            return File.ReadLines(_csvPath).Skip(1)
                .Select(s => s.Split(";"))
                .Select(sa => new Product()
                {
                    Id = sa[0],
                    Name = sa[1],
                    Price = Int32.Parse(sa[2]),
                    Producer = new Producer() { ProducerName = sa[3] },
                    Shops = new List<Shop>() { new Shop(sa[4]) { ShopName = sa[4] } }
                })
                .ToList();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(string id)
        {
            return _products.First(p => p.Id == id);
        }

        public void Delete(Product product)
        {
            _products.Remove(product);
            Save();
        }

        public void Insert(Product product)
        {
            _products.Add(product);
            Save();
        }

        public void Save()
        {
            var jsonString = JsonSerializer.SerializeToUtf8Bytes(_products);
            File.WriteAllBytes(_jsonPath, jsonString);
        }
    }
}

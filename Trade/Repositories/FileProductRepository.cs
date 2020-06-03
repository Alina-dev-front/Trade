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
            _csvPath = GetUserFileDirectory();
            _products = GetProducts();
            _jsonPath = GetUserJsonPath();
        }

        public List<Product> GetProducts() 
        {
            return File.ReadLines(_csvPath).Skip(1)
                .Select(s => s.Split(","))
                .Select(sa => new Product()
                {
                    Name = sa[0],
                    Price = Int32.Parse(sa[1]),
                    Producer = new Producer() { ProducerName = sa[2] },
                    Shops = new List<Shop>() { new Shop() { ShopName = sa[3] } }
                })
                .ToList();
        }

        public List<Product> GetAll()
        {
            return _products;
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

        

        public static string GetUserFileDirectory()
        {
            static string GetUserHomePath()
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify);
            }

            var homePath = GetUserHomePath();
            var programPath = Path.Combine(homePath, ".TradeProject");

            if (!Directory.Exists(programPath))
            {
                Directory.CreateDirectory(programPath);
            }

            return Path.Combine(programPath, "ProductData.csv");
        }

        public static string GetUserJsonPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var programPath = Path.Combine(path, ".TradeProject");
            var jsonPath = Path.Combine(programPath, "Products.json");
            return jsonPath;
        }
    }
}

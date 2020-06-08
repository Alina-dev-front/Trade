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
                .Select(s => s.Split(";"))
                .Select(sa => new Product()
                {
                    Id = Int32.Parse(sa[0]),
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

        public Product GetById(long id)
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

        public void ShowAllProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine("Name of product: " + product.Name);
                Console.WriteLine("Price: " + product.Price);
                Console.WriteLine("Made by: " + product.Producer.ProducerName);
                Console.WriteLine("Shops where product is available:");
                foreach (var shops in product.Shops)
                {
                    foreach (var shop in shops.ListShopName)
                    {
                        Console.WriteLine(shop);
                    }
                }
                Console.WriteLine("____________________________________________________");
            }
        }

        public void SearchProductByName(List<Product> products)
        {
            static string GetUserSearchTerm()
            {
                Console.WriteLine("Insert search term. You can insert full word or just a part of it: ");
                string searchTerm = Console.ReadLine();
                return searchTerm;
            }

            var searchTerm = GetUserSearchTerm();
            foreach (var product in products)
            {
                if (product.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                    Console.WriteLine(product);
            }
        }

        public void ShowProducersAndTheirProducts(List<Product> products)
        {
            var numberOfProductForEachProducer = products.GroupBy(p => p.Producer.ProducerName).Select(g =>
                    new { Producer = g.Key, Count = g.Count() }).ToList();
            foreach (var i in numberOfProductForEachProducer)
            {
                Console.WriteLine(i);
            }
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

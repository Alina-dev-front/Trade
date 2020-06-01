using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using Trade.Repositories;
using Trade.Models;


namespace Trade
{
    class JSONFileHandler
    {
        /*Need to keep files*/
        public void Run(List<Product> products)
        {

            /*var products = new List<Product>()
            {
                new Product() {Name = "Banan", Price = 42, Producer = new Product().Producer, Shop = new Product().Shop},
                new Product() {Name = "Ananas", Price = 30, Producer = new Product().Producer, Shop = new Product().Shop}
            };*/
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var programPath = Path.Combine(path, ".TradeProject");
            var jsonPath = Path.Combine(programPath, "Products.json");

            SaveToJson(jsonPath, products);

            var records = LoadFromJson<Product>(jsonPath);

            foreach (var product in records)
            {
                Console.WriteLine(product);
            }
        }

        public void SaveToJson(string jsonPath, List<Product> products)
        {
            var jsonString = JsonSerializer.SerializeToUtf8Bytes(products);
            File.WriteAllBytes(jsonPath, jsonString);
        }

        public static IEnumerable<T> LoadFromJson<T>(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);

            var records = JsonSerializer.Deserialize<List<T>>(jsonString);
            return records;
        }

        public void SaveToJson<T>(string filePath, IEnumerable<T> items)
        {
            var jsonString = JsonSerializer.SerializeToUtf8Bytes(items);
            File.WriteAllBytes(filePath, jsonString);
        }
    }
}

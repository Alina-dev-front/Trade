using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Trade
{
    class JSONFileHandler
    {
        /*Need to keep files*/
        public void Run()
        {

            var products = new List<Product>()
            {
                new Product() {Id = 1, Name = "Martin", Price = 42, Producer = new Product().Producer, Shop = new Product().Shop},
                new Product() {Id = 2, Name = "Anna", Price = 30, Producer = new Product().Producer, Shop = new Product().Shop}
            };
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

        public static void SaveToJson(string jsonPath, List<Product> products)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> LoadFromJson<T>(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);

            var records = JsonSerializer.Deserialize<List<T>>(jsonString);
            return records;
        }

        public static void SaveToJson<T>(string filePath, List<T> items)
        {
            var jsonString = JsonSerializer.SerializeToUtf8Bytes(items);
            File.WriteAllBytes(filePath, jsonString);
        }
    }
}

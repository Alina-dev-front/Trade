using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Collections.Immutable;

namespace Trade
{
    public class FileProductRepository : IProductRepository
    {
        public FileProductRepository()
        {
        }

        public List<Product> LoadProductsFromCSV(string csvPath)
        {
           /* Producer producer4 = new Producer() { ProducerId = 3, ProducerName = "Nivea" };
*/
            /*new Product() { Id = 3, Name = "Ananas", Price = 89, Producer = producer4, Shop = "Ica" },
*/
            var products = File.ReadLines(csvPath).Skip(1)
                 .Select(s => s.Split(","))
                 .Select(sa => new Product() { Id = Int32.Parse(sa[0]), Name = sa[1], Price = Int32.Parse(sa[2]), Producer = new Producer() {ProducerName = sa[3]} , Shop = sa[4] })
                 .ToList();
/*
            foreach (var product in products)
            {
                Console.WriteLine(product.Producer.ProducerName);
                Console.WriteLine(product);
                
            }*/
            return products;
        }

        public Product GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public void InsertProductInFile(string filePath, string text)
        {
            File.AppendAllText(filePath, text);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}

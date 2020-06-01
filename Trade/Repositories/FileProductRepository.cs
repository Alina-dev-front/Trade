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
            var products = File.ReadLines(csvPath).Skip(1)
                 .Select(s => s.Split(","))
                 .Select(sa => new Product() { Name = sa[0], Price = Int32.Parse(sa[1]), Producer = new Producer() { ProducerName = sa[2] }, Shop = new Shop() { ShopName = sa[3] } })
                 .ToList();
            return products;
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Trade
{
    public interface IProductRepository
    {
        List<Product> LoadProductsFromCSV(string csvPath);
        void Delete(Product product);
        void InsertProductInFile(string filePath, string text);
        void Update();
        void Save();
    }
}

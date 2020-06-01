using System;
using System.Collections.Generic;
using System.Text;
using Trade.Models;

namespace Trade.Repositories
{
    public interface IProductRepository
    {
        void Delete(Product product);
        void InsertProductInFile(string filePath, string text);
        void Update();
        void Save();
        List<Product> GetAll();
    }
}

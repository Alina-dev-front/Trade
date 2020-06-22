using System;
using System.Collections.Generic;
using System.Text;
using Trade.Models;

namespace Trade.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(string id);
        void Delete(Product product);
        void Insert(Product product);
        void Save();
    }
}

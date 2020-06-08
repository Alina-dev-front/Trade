using System;
using System.Collections.Generic;
using System.Text;
using Trade.Models;

namespace Trade.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(long id);
        void Delete(Product product);
        void Insert(Product product);
        void Save();
        void ShowAllProducts(List<Product> products);
        void SearchProductByName(List<Product> products);
        void ShowProducersAndTheirProducts(List<Product> products)
    }
}

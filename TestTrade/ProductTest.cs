using System;
using Trade.Repositories;
using Trade.Models;
using Xunit;
using System.Collections.Generic;

namespace TestTrade
{
    public class ProductTest
    {
        [Fact]
        public void SearchFirstProductsName()
        {
            IProductRepository productRepository = new FileProductRepository();
            List<Product> products = productRepository.GetAll();

            string temp = products[0].Name;

            Assert.Equal("Milk", temp);
        }

        [Fact]
        public void SearchFirstProductsNameFail()
        {
            IProductRepository productRepository = new FileProductRepository();
            List<Product> products = productRepository.GetAll();

            string temp = products[0].Name;

            Assert.NotEqual("Apple", temp);
        }

        [Fact]
        public void CountNumberOfProductsInCsv()
        {
            IProductRepository productRepository = new FileProductRepository();
            List<Product> products = productRepository.GetAll();

            Assert.Equal(12, products.Count);
        }


    }
}

using System;
using Trade.Repositories;
using Trade.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualBasic;
using FluentAssertions.Collections;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

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


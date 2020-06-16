using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trade.Models;
using Trade.Repositories;

namespace ProductMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IProductRepository productRepository = new FileProductRepository();
            var products = productRepository.GetAll();

            List<string> productNameCsv = new List<string>();
            foreach (var prod in products)
            {
                productNameCsv.Add(prod.Name);
            }

            var test = new List<string>() { "Milk", "Shampoo", "Chocolate Bar", "Cucumber", "Potato", "Coca-Cola", 
                "Banan", "Ananas", "Beef", "Dog's Food", "Liquid Soap", "Tomato" };

            CollectionAssert.AreEqual(productNameCsv, test);
        }
    }
}

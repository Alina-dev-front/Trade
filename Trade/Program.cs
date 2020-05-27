using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;

namespace Trade
{
    class Program
    {
        static void Main(string[] args)
        {
            var homePath = GetUserHomePath();
            var programPath = Path.Combine(homePath, ".TradeProject");
            if (!Directory.Exists(programPath))
            {
                Directory.CreateDirectory(programPath);
            }
            var csvPath = Path.Combine(programPath, "ProductData.csv");

            IProductRepository productRepository = new FileProductRepository();
            var products = productRepository.LoadProductsFromCSV(csvPath);
            
            new FileProductRepository().InsertProductInFile(csvPath, Environment.NewLine + "15,Apelsin,20,GoodFood,Ica");

            foreach (var product in products)
            {
                product.Producer.ProducerName = "xuy";
                Console.WriteLine(product.Producer.ProducerName);
                Console.WriteLine(product);
            }

            Shop shop1 = new Shop(1, "Ica");
            Shop shop2 = new Shop(2, "Coop");
            Shop shop3 = new Shop(3, "Metro");

            Producer producer1 = new Producer() { ProducerId = 0, ProducerName = "Arla" };
            Producer producer2 = new Producer() { ProducerId = 1, ProducerName = "GoodFood" };
            Producer producer3 = new Producer() { ProducerId = 2, ProducerName = "Nestle" };
            Producer producer4 = new Producer() { ProducerId = 3, ProducerName = "Nivea" };

/*
            var list = prods.Where(s => s.Producer.ProducerId == 0);*/


            Console.WriteLine(products.Count);
            Console.WriteLine("Hello World!");
        }


        static string GetUserHomePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify);
        }

    }
}

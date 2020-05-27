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

            var list = products.Where(s => s.Shop.ShopName == "Ica");
            foreach (var prod in list)
            {
                Console.WriteLine(prod.Name);
            }

            Console.WriteLine(products.Count);
            Console.WriteLine("Hello World!");
        }


        static string GetUserHomePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify);
        }

    }
}

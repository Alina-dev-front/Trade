using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Text.Json;

namespace Trade
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvPath = GetUserFileDirectory();
            var jsonPath = GetUserJsonPath();

            IProductRepository productRepository = new FileProductRepository();
            var products = productRepository.LoadProductsFromCSV(csvPath);


            Console.WriteLine("Product Manager Project in C#\r");
            Menu();
            string inputValue = Console.ReadLine();
            while (inputValue != "q")
            {
                switch (inputValue)
                {
                    case "1":
                        foreach(var product in products)
                        {
                            Console.WriteLine(product);
                        }
                        inputValue = Console.ReadLine();
                        break;
                    case "2":
                        SearchProductMinPrice(products);
                        inputValue = Console.ReadLine();
                        break;
                    case "3":
                        string productProperties = GetProductFromUser();
                        new FileProductRepository().InsertProductInFile(csvPath, Environment.NewLine + productProperties);
                        Console.WriteLine("The product has been successfully added to the list. Open the CSV-file to check.");
                        inputValue = Console.ReadLine();
                        break;
                    case "4":
                        var searchTerm = GetUserSearchTerm();
                        foreach (var product in products)
                        {
                            if (product.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                                Console.WriteLine(product);
                        }
                        inputValue = Console.ReadLine();
                        break;
                    case "q":
                        break;
                    default:
                        Console.WriteLine("This option is not defined. Try again!");
                        inputValue = Console.ReadLine();
                        break;
                }
            }

            var list = products.Where(s => s.Shop.ShopName == "Ica");
            foreach (var prod in list)
            {
                Console.WriteLine(prod.Name);
            }

            SaveToJson(jsonPath, list);

            Console.WriteLine("Hello World!");
        }


        public static void Menu()
        {
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Choose one option and press Enter:");
            Console.WriteLine("Press 1 if you want to see all the products");
            Console.WriteLine("Press 2 if you want to choose the cheapest products");
            Console.WriteLine("Press 3 if you want to add product to the list");
            Console.WriteLine("Press 4 if you want to start searching product");
            Console.WriteLine("Press q to exit");
        }

        public static string GetUserSearchTerm()
        {
            Console.WriteLine("Insert search term. You can insert full word or just a part of it: ");
            string searchTerm = Console.ReadLine();
            return searchTerm;
        }

        public static string GetProductFromUser()
        {
            string[] paramNames = { "name", "price", "producer", "shop" };
            string parameters = "";
            for (var i = 0; i <= paramNames.Count() - 1; i++)
            {
                parameters += GetParameters(paramNames[i]);
                if (i < paramNames.Count() - 1)
                {
                    parameters += ",";
                }
            }
            return parameters;
        }

        public static string GetParameters(string name)
        {
            Console.WriteLine("Insert product's " + name + " and press Enter: ");
            string parameter = Console.ReadLine();
            string parameterFromUser = parameter.ToString();
            return parameterFromUser;
        }

        public static string GetUserFileDirectory()
        {
            var homePath = GetUserHomePath();
            var programPath = Path.Combine(homePath, ".TradeProject");
            if (!Directory.Exists(programPath))
            {
                Directory.CreateDirectory(programPath);
            }
            var csvPath = Path.Combine(programPath, "ProductData.csv");
            return csvPath;

            static string GetUserHomePath()
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify);
            }
        }

        public static string GetUserJsonPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var programPath = Path.Combine(path, ".TradeProject");
            var jsonPath = Path.Combine(programPath, "Products.json");
            return jsonPath;
        }

        public static void SaveToJson(string jsonPath, IEnumerable<Product> products)
        {
            var jsonString = JsonSerializer.SerializeToUtf8Bytes(products);
            File.WriteAllBytes(jsonPath, jsonString);
        }

        public static int GetMaxPriceFromUser()
        {
            Console.WriteLine("Insert maximum price: ");
            int maxPrice = Int32.Parse(Console.ReadLine());
            return maxPrice;
        }

        public static void SearchProductMinPrice(List<Product> products)
        {
            var maxPrice = GetMaxPriceFromUser();
            var query = from p in products
                        where p.Price < maxPrice
                        select p;
            Console.WriteLine("Price is lower than " + maxPrice.ToString() + " kr: ");
            foreach (var product in query)
            {
                Console.WriteLine(product.Name + "  " + product.Price);
            }
        }
    }
}

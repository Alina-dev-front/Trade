using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using Trade.Models;
using Trade.Repositories;

namespace Trade
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepository = new FileProductRepository();
            var products = productRepository.GetAll();

/*            var shops = new List<Shop>
            {
                new Shop() { ShopId = 0, ShopName = "Ica" },
                new Shop() { ShopId = 1, ShopName = "Coop" },
                new Shop() { ShopId = 2, ShopName = "Metro" }
            };

            List<Product> stock = new List<Product>();
            Product product1 = new Product() { Name = "Apple", Price = 32, Producer = new Producer() { ProducerName = "Max" }, Shops = shops };
            Product product2 = new Product() { Name = "Pear", Price = 84, Producer = new Producer() { ProducerName = "Mimi" }, Shops = shops };
            stock.Add(product1);
            stock.Add(product2);

            Console.WriteLine(product1);*/



            Console.WriteLine("Product Manager Project in C#\r");
            Menu();
            string inputValue = Console.ReadLine();
            while (inputValue != "q")
            {
                switch (inputValue)
                {
                    case "1":
                        foreach (var product in products)
                        {
                            Console.WriteLine("name of product:");
                            Console.WriteLine(product.Name);
                            Console.WriteLine("price:");
                            Console.WriteLine(product.Price);
                            Console.WriteLine("Made by:");
                            Console.WriteLine(product.Producer.ProducerName);
                            Console.WriteLine("Shops where product is available:");
                            foreach (var shops in product.Shops)
                            {
                                foreach (var shop in shops.ListShopName)
                                { 
                                    Console.WriteLine(shop);
                                }
                            }
                            Console.WriteLine("____________________________________________________");
                        }
                        inputValue = Console.ReadLine();
                        break;
                    case "2":
                        SearchProductMinPrice(products);
                        inputValue = Console.ReadLine();
                        break;
                    case "3":
                        List<string> parametersFromUser = GetProductParametersFromUser();
                        Product productFromUser = new Product();
                        productFromUser.Name = parametersFromUser[0];
                        productFromUser.Price = Int32.Parse(parametersFromUser[1]);
                        productFromUser.Producer = new Producer() { ProducerName = parametersFromUser[2] };
                        productRepository.Insert(productFromUser);
                        Console.WriteLine("The product has been successfully added to the list. Open the JSON-file to check.");
                        inputValue = Console.ReadLine();
                        break;
                    case "4":
                        var milk = products[0];
                        productRepository.Delete(milk);
                        inputValue = Console.ReadLine();
                        break;
                    case "5":
                        var searchTerm = GetUserSearchTerm();
                        foreach (var product in products)
                        {
                            if (product.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                                Console.WriteLine(product);
                        }
                        inputValue = Console.ReadLine();
                        break;
                    case "6":
                        var numberOfProductForEachProducer = products.GroupBy(p => p.Producer.ProducerName).Select(g =>
                    new { Producer = g.Key, Count = g.Count() }).ToList();
                        foreach (var i in numberOfProductForEachProducer)
                        {
                            Console.WriteLine(i);
                        }
                        inputValue = Console.ReadLine();
                        break;
                    case "7":
                        
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
        }

        public static void Menu()
        {
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Choose one option and press Enter:");
            Console.WriteLine("Press 1 if you want to see all the products");
            Console.WriteLine("Press 2 if you want to choose the cheapest products");
            Console.WriteLine("Press 3 if you want to add product to the list");
            Console.WriteLine("Press 4 if you want to delete product from the list");
            Console.WriteLine("Press 5 if you want to start searching product");
            Console.WriteLine("Press 6 if you want to find all producers and their products");
            Console.WriteLine("Press 7 if you want to find all shops that have products in their warehouse");
            Console.WriteLine("Press q to exit");
        }

        public static string GetUserSearchTerm()
        {
            Console.WriteLine("Insert search term. You can insert full word or just a part of it: ");
            string searchTerm = Console.ReadLine();
            return searchTerm;
        }

        public static List<string> GetProductParametersFromUser()
        {
            static string GetParameters(string name)
            {
                Console.WriteLine("Insert product's " + name + " and press Enter: ");
                string parameter = Console.ReadLine();
                return parameter;
                
            }
            string[] paramNames = { "name", "price", "producer" };
            List<string> parametersFromUser = new List<string>();
            for (var i = 0; i <= paramNames.Count() - 1; i++)
            {
                parametersFromUser.Add(GetParameters(paramNames[i]));
            }
            return parametersFromUser;
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
            var query = products.Where(p => p.Price < maxPrice).Take(10);
            Console.WriteLine("These products have price lower than " + maxPrice.ToString() + " kr:");
            foreach (var product in query)
            {
                Console.WriteLine(product.Name + "  " + product.Price);
            }
        }
    }
}

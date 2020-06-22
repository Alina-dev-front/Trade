using System;
using System.Collections.Generic;
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

            Console.WriteLine("Product Manager Project in C#\r");
            Menu();
            string inputValue = Console.ReadLine();
            while (inputValue != "q")
            {
                switch (inputValue)
                {
                    case "1":
                        ShowAllProducts(products);
                        inputValue = Console.ReadLine();
                        break;
                    case "2":
                        SearchProductMinPrice(products);
                        inputValue = Console.ReadLine();
                        break;
                    case "3":
                        AddProductWithUsersParameters(products, productRepository);
                        inputValue = Console.ReadLine();
                        break;
                    case "4":
                        DeleteProduct(products, productRepository);
                        inputValue = Console.ReadLine();
                        break;
                    case "5":
                        SearchProductByName(products);
                        inputValue = Console.ReadLine();
                        break;
                    case "6":
                        ShowProducersAndTheirProducts(products);
                        inputValue = Console.ReadLine();
                        break;
                    case "7":
                        IShopsRepository shopsRepository = new ShopsRepository();
                        shopsRepository.ShowProductsInEachShop(products);
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

        public static void AddProductWithUsersParameters(List<Product> products, IProductRepository productRepository)
        {
            List<string> parametersFromUser = GetProductParametersFromUser();
            Product productFromUser = new Product
            {
                Id = products.Last().Id + 1,
                Name = parametersFromUser[0],
                Price = Int32.Parse(parametersFromUser[1]),
                Producer = new Producer() { ProducerName = parametersFromUser[2] },
                Shops = new List<Shop>() { new Shop(parametersFromUser[3]) { ShopName = parametersFromUser[3] } }
            };
            productRepository.Insert(productFromUser);
            Console.WriteLine("The product has been successfully added to the list. Open the JSON-file to check.");
        }

        public static void DeleteProduct(List<Product> products, IProductRepository productRepository)
        {
            Console.WriteLine("Choose product which you want to delete. Type product's ID below.");
            foreach (var product in products)
            {
                Console.WriteLine($"Id: " + product.Id + " Name: " + product.Name + " Price: " + product.Price + " Producer: " + product.Producer.ProducerName);
            }
            var input = Console.ReadLine();
            int idProductToDelete = Int32.Parse(input);
            Product productToDelete = productRepository.GetById(idProductToDelete.ToString());
            productRepository.Delete(productToDelete);
            Console.WriteLine("The product has been successfully deleted from the list. Open the JSON-file to check.");
        }

        public static List<string> GetProductParametersFromUser()
        {
            static string GetParameters(string name)
            {
                Console.WriteLine("Insert product's " + name + " and press Enter: ");
                string parameter = Console.ReadLine();
                return parameter;
                
            }
            string[] paramNames = { "name", "price", "producer", "shop" };
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

        public static void ShowProducersAndTheirProducts(List<Product> products)
        {
            var numberOfProductForEachProducer = products.GroupBy(p => p.Producer.ProducerName).Select(g =>
                    new { Producer = g.Key, Count = g.Count() }).ToList();
            foreach (var i in numberOfProductForEachProducer)
            {
                Console.WriteLine(i);
            }
        }

        public static void SearchProductByName(List<Product> products)
        {
            static string GetUserSearchTerm()
            {
                Console.WriteLine("Insert search term. You can insert full word or just a part of it: ");
                string searchTerm = Console.ReadLine();
                return searchTerm;
            }

            var searchTerm = GetUserSearchTerm();
            foreach (var product in products)
            {
                if (product.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                    Console.WriteLine(product);
            }
        }

        public static void ShowAllProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine("Name of product: " + product.Name);
                Console.WriteLine("Price: " + product.Price);
                Console.WriteLine("Made by: " + product.Producer.ProducerName);
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
        }
    }
}

using IF5W4R.Models;
using IF5W4R.Services;

namespace IF5W4R
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IFileService fileService = new FileService();
            IProductDisplayService productDisplayService = new ProductDisplayService();
            IProductService productService = new ProductService(fileService, productDisplayService);
            string filePath = "Resources/products.json";
            try
            {
                List<Product> productList = await fileService.ReadFromJsonFileAsync<List<Product>>(filePath);
                productService.LoadProducts(productList);
                Console.WriteLine("Product registry application loaded!");
                Console.WriteLine("For help please write: help");
                while (true)
                {
                    Console.Write("Enter command: ");
                    string input = Console.ReadLine()?.Trim().ToLower();

                    switch (input)
                    {
                        case "add":
                            AddProduct(productService);
                            break;
                        case "list":
                            productService.ListAllProducts();
                            break;
                        case "help":
                            Console.WriteLine("Commands:");
                            Console.WriteLine("add - Add a new product");
                            Console.WriteLine("list - List all products");
                            Console.WriteLine("list <Category> - List products by category");
                            break;
                        default:
                            if (input.StartsWith("list "))
                            {
                                string categoryParam = input.Substring(5);
                                productService.ListProductsByCategory(categoryParam);
                            }
                            else
                            {
                                Console.WriteLine("Invalid command. Type 'help' for available commands.");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine();
        }

        static void AddProduct(IProductService productService)
        {
            try
            {
                Console.Write("Enter product name: ");
                string name = Console.ReadLine();
                Console.Write("Enter product category: ");
                string category = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category))
                {
                    Console.WriteLine("Product name and category cannot be empty.");
                    return;
                }

                Console.Write("Enter product quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a valid positive integer.");
                    return;
                }

                Console.Write("Enter product price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                {
                    Console.WriteLine("Invalid price. Please enter a valid positive decimal number.");
                    return;
                }

                productService.AddProduct(name, category, quantity, price);
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
            }
        }

    }
}

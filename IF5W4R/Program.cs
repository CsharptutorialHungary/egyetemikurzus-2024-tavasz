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
            ICommandHandler commandHandler = new CommandHandler(productService);
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
                    commandHandler.HandleCommand(input);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}

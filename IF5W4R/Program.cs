using System;
using System.IO;
using System.Threading.Tasks;
using IF5W4R.Models;
using IF5W4R.Services;

namespace IF5W4R
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileService fileService = new FileService();
            ProductService productService = new ProductService();
            string filePath = "Resources/products.json";
            try
            {
                List<Product> productList = await fileService.ReadFromJsonFileAsync<List<Product>>(filePath);
                productService.LoadProducts(productList);

                productService.DisplayProducts();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}

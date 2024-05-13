using System.Text.Json;

using IF5W4R.Models;

namespace IF5W4R.Services
{
    public class ProductService : IProductService
    {
        private List<Product> products;
        private readonly string filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Resources", "products.json");
        private readonly IFileService fileService;
        private readonly IProductDisplayService productDisplayService;

        public ProductService(IFileService fileService, IProductDisplayService productDisplayService)
        {
            this.fileService = fileService;
            this.productDisplayService = productDisplayService;
            products = new List<Product>();
        }

        public void LoadProducts(List<Product> productList)
        {
            products.AddRange(productList);
        }

        public void AddProduct(string name, string category, int quantity, decimal price)
        {
            products.Add(new Product(name, category, quantity, price));
            SaveProductsToFile();
        }

        public void SaveProductsToFile()
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
        }

        public void ListAllProducts()
        {
            productDisplayService.DisplayProducts(products);
        }

        public void FilterByCategory(string category)
        {
            var filteredProducts = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            if (filteredProducts.Any())
            {
                Console.WriteLine($"Products in category '{category}':");
                productDisplayService.DisplayProducts(filteredProducts);
            }
            else
            {
                Console.WriteLine($"No products found in category '{category}'.");
            }
        }

        public void ListProducts(string field, string order)
        {
            IEnumerable<Product> orderedProducts;

            switch (field)
            {
                case "-n":
                    orderedProducts = products.OrderBy(p => p.Name);
                    break;
                case "-c":
                    orderedProducts = products.OrderBy(p => p.Category);
                    break;
                case "-q":
                    orderedProducts = products.OrderBy(p => p.Quantity);
                    break;
                case "-p":
                    orderedProducts = products.OrderBy(p => p.Price);
                    break;
                default:
                    Console.WriteLine("Invalid field. Type 'help' for available commands.");
                    return;
            }

            if (order == "-asc")
            {
                productDisplayService.DisplayProducts(orderedProducts.ToList());
            }
            else if (order == "-desc")
            {
                productDisplayService.DisplayProducts(orderedProducts.Reverse().ToList());
            }
            else
            {
                Console.WriteLine("Invalid order. Type 'help' for available commands.");
            }
        }

    }
}

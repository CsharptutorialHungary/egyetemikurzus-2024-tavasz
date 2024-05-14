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
            int id = products.Count > 0 ? products.Max(p => p.ID) + 1 : 1;
            products.Add(new Product(id, name, category, quantity, price));
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

        public void DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.ID == id);
            if (product != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Are you sure you want to delete the product with ID {id}? (yes/no)");
                Console.ForegroundColor = ConsoleColor.Gray;

                string confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "yes")
                {
                    Console.WriteLine("Product deletion cancelled.");
                    return;
                }
                else
                {
                    products.Remove(product);
                    SaveProductsToFile();
                    Console.WriteLine($"Product with ID {id} deleted successfully.");
                }
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.ID == id);
        }

        public void UpdateProduct(int id, string name, string category, int quantity, decimal price)
        {
            var product = products.FirstOrDefault(p => p.ID == id);
            if (product != null)
            {
                products.Remove(product);
                products.Add(new Product(id, name, category, quantity, price));
                SaveProductsToFile();
                Console.WriteLine($"Product with ID {id} updated successfully.");
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
        }
    }
}

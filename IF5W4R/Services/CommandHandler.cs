namespace IF5W4R.Services
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IProductService _productService;

        public CommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public void HandleCommand(string command)
        {
            switch (command)
            {
                case "add":
                    AddProduct();
                    break;
                case "list":
                    _productService.ListAllProducts();
                    break;
                case "help":
                    Console.WriteLine("Commands:");
                    Console.WriteLine("add - Add a new product");
                    Console.WriteLine("list - List all products");
                    Console.WriteLine("list <Category> - List products by category");
                    break;
                default:
                    if (command.StartsWith("list "))
                    {
                        string categoryParam = command.Substring(5);
                        _productService.ListProductsByCategory(categoryParam);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Type 'help' for available commands.");
                    }
                    break;
            }
        }

        private void AddProduct()
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

                _productService.AddProduct(name, category, quantity, price);
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
            }
        }
    }
}

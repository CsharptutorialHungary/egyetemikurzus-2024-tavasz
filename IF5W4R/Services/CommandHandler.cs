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
            if (command.StartsWith("list"))
            {
                string[] commandParts = command.Split(' ');
                if (commandParts.Length == 3)
                {
                    string field = commandParts[1];
                    string order = commandParts[2];
                    _productService.ListProducts(field, order);
                }
                else if (commandParts.Length == 1 && command == "list")
                {
                    _productService.ListAllProducts();
                }
                else if (commandParts.Length == 2)
                {
                    if (commandParts[1] == "-n" || commandParts[1] == "-c" || commandParts[1] == "-q" || commandParts[1] == "-p")
                    {
                        Console.WriteLine("Invalid number of arguments. Type 'help' for available commands.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid field. Type 'help' for available commands.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid number of arguments. Type 'help' for available commands.");
                }
            }
            else if (command == "exit")
            {
                Environment.Exit(0);
            }
            else if (command == "add")
            {
                AddProduct();
            }
            else if (command == "help" || command == "h")
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("\tadd - Add a new product");
                Console.WriteLine("\tlist - List all products");
                Console.WriteLine("\tlist <-n/c/q/p> <-asc/desc> - List products ordered by field (name, category, quantity, price) in order (asc for ascending, desc for descending)");
                Console.WriteLine("\tfilter [Category] - List products by category");
                Console.WriteLine("\tupdate [ID] - Update a product by ID");
                Console.WriteLine("\tdelete [ID] - Delete a product by ID");
                Console.WriteLine("\texit - Exit the program");
            }
            else if (command.StartsWith("filter "))
            {
                string categoryParam = command.Substring(7);
                _productService.FilterByCategory(categoryParam);
            }
            else if (command.StartsWith("delete "))
            {
                string[] commandParts = command.Split(' ');
                if (commandParts.Length == 2)
                {
                    if (int.TryParse(commandParts[1], out int id))
                    {
                        _productService.DeleteProduct(id);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number of arguments. Type 'help' for available commands.");
                }
            }
            else if (command.StartsWith("update "))
            {
                string[] commandParts = command.Split(' ');
                if (commandParts.Length == 2)
                {
                    if (int.TryParse(commandParts[1], out int id))
                    {
                        UpdateProduct(id);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number of arguments. Type 'help' for available commands.");
                }
            }
            else
            {
                Console.WriteLine("Invalid command. Type 'help' for available commands.");
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

        private void UpdateProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                Console.Write($"Current product name: {product.Name}\nNew product name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Product name cannot be empty or null.");
                    return;
                }
                Console.Write($"Current product category: {product.Category}\nNew product category: ");
                string category = Console.ReadLine();
                if (string.IsNullOrEmpty(category))
                {
                    Console.WriteLine("Product category cannot be empty or null.");
                    return;
                }
                Console.Write($"Current product quantity: {product.Quantity}\nNew product quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a valid positive integer.");
                    return;
                }
                Console.Write($"Current product price: {product.Price}\nNew product price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                {
                    Console.WriteLine("Invalid price. Please enter a valid positive decimal number.");
                    return;
                }
                _productService.UpdateProduct(id, name, category, quantity, price);
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
        }
    }
}

using System;
using System.Collections.Generic;

using IF5W4R.Models;

namespace IF5W4R.Services
{
    public class ProductService
    {
        private List<Product> products;

        public ProductService()
        {
            products = new List<Product>();
        }

        public void LoadProducts(List<Product> productList)
        {
            products.AddRange(productList);
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Products:");
            DisplayTableHeader();
            foreach (var product in products)
            {
                DisplayProductRow(product);
            }
        }

        private void DisplayTableHeader()
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("|              Name              |    Category    | Quantity |      Price      |");
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayProductRow(Product product)
        {
            string formattedName = FormatString(product.Name, 30);
            string formattedCategory = FormatString(product.Category, 14);
            string formattedQuantity = string.Format("{0,8}", product.Quantity);
            string formattedPrice = string.Format("{0,15:C}", product.Price);

            Console.WriteLine($"| {formattedName} | {formattedCategory} | {formattedQuantity} | {formattedPrice} |");
        }

        private string FormatString(string input, int width)
        {
            return input.PadRight(width).Substring(0, width);
        }
    }
}

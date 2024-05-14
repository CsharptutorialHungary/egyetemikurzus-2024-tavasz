using IF5W4R.Models;

namespace IF5W4R.Services
{
    public interface IProductService
    {
        void LoadProducts(List<Product> productList);
        void AddProduct(string name, string category, int quantity, decimal price);
        void ListAllProducts();
        void FilterByCategory(string category);
        void ListProducts(string field, string order);
        void DeleteProduct(int id);
    }
}

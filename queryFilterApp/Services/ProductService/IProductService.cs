using queryFilterApp.Models;
using queryFilterApp.Services.ProductService.DTOs;

namespace queryFilterApp.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);
    }
}

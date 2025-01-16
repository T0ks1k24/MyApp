using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<bool> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(int id, Product product);
    Task<bool> RemoveProductAsync(int id);
}

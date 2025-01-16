using Application.DTOs;
using Application.DTOs.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync(ProductFilterDto filter);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<bool> AddProductAsync(AddProductDto addProductDto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> RemoveProductAsync(int id);
    }
}

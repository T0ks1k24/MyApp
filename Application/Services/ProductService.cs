using Application.DTOs;
using Application.DTOs.Product;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    //Get all product
    public async Task<IEnumerable<ProductDto>> GetAllProductAsync(ProductFilterDto filter)
    {
        var products = await _productRepository.GetAllProductAsync();
        if (products == null) return null;

        if(!string.IsNullOrWhiteSpace(filter.Name))
            products = products.Where(p => p.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
        if (filter.MinPrice.HasValue)
            products = products.Where(p => p.Price >= filter.MinPrice.Value);
        if(filter.MaxPrice.HasValue)
            products = products.Where(p => p.Price <= filter.MaxPrice.Value);

        products = filter.SortBy?.ToLower() switch
        {
            "name" => filter.SortDescending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name),
            "price" => filter.SortDescending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price),
            "categoryname" => filter.SortDescending ? products.OrderByDescending(p => p.Category.Name) : products.OrderBy(p => p.Category.Name),
            _ => filter.SortDescending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id)
        };

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    //Get product by id
    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return null;

        return _mapper.Map<ProductDto>(product);
    }

    //Add product
    public async Task<bool> AddProductAsync(AddProductDto addProductDto)
    {
        var product = new Product
        {
            Name = addProductDto.Name,
            Description = addProductDto.Description,
            Price = addProductDto.Price,
            StockQuantity = addProductDto.StockQuantity,
            ImageUrl = addProductDto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            CategoryId = addProductDto.CategoryId
        };

        bool check = await _productRepository.AddProductAsync(product);
        return check;
    }

    //Update product
    public async Task<bool> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return false;

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.StockQuantity = updateProductDto.StockQuantity;
        product.CategoryId = updateProductDto.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        bool check = await _productRepository.UpdateProductAsync(id, product);
        return check;
    }

    //Remove product
    public async Task<bool> DeleteProductAsync(int id)
    {
        bool check = await _productRepository.RemoveProductAsync(id);
        return check;
    }


}


using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Category, CategoryDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<CartItem, CartItemDto>()
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
           .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<AddCartItemDto, CartItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));



        }
    }
}

using AutoMapper;
using OrderManagementAPI.Models;
using OrderManagementAPI.DTOs;

namespace OrderManagementAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>();

            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderItem, OrderItemReadDTO>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(d => d.DiscountPercent, opt => opt.MapFrom(src => src.Product.DiscountPercent));


            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderItemCreateDTO, OrderItem>();

            CreateMap<ProductDiscountDTO, Product>();
        }
    }
}

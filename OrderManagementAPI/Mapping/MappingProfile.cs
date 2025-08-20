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
            CreateMap<OrderItem, OrderItemReadDTO>();

            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderItemCreateDTO, OrderItem>();

            CreateMap<ProductDiscountDTO, Product>();
        }
    }
}

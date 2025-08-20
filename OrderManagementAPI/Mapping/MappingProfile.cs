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
            CreateMap<OrderCreateDTO, Order>();
        }
    }
}

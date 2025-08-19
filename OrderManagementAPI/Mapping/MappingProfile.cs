using AutoMapper;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReadDTO>()
        }
    }
}

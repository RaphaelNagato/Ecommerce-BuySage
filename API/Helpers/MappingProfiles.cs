using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(p => p.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
            .ForMember(p => p.ProductType, o => o.MapFrom(p => p.ProductType.Name));
        }
    }
}
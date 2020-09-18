using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        { 
            CreateMap<Product,ProductToReturnDto>()
            .ForMember(o => o.ProductBrand, p => p.MapFrom( q=>q.ProductBrand.Name))
            .ForMember(o => o.ProductType, p => p.MapFrom( q=>q.ProductType.Name))
            .ForMember(o => o.PictureUrl, p => p.MapFrom<ProductUrlResolver>());
        }    
    }
}
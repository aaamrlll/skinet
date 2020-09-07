using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>().ForMember(prodTrtrnDto => prodTrtrnDto.ProductBrand, options =>
            {
                options.MapFrom(Product => Product.ProductBrand.Name);
            })
            .ForMember(prodTrtrnDto => prodTrtrnDto.ProductType, options =>
            {
                options.MapFrom(Product => Product.ProductType.Name);
            })
            .ForMember(prodTrtrnDto => prodTrtrnDto.PictureUrl, options =>
            {
                options.MapFrom<ProductUrlResolver>();
            });
        }
    }
}
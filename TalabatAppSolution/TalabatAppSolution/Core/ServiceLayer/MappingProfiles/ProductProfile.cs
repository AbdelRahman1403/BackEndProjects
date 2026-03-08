using AutoMapper;
using DomainLayer.Models.BasketModels;
using DomainLayer.Models.ProductModels;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.BasketDots;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MappingProfiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Products , ProductDto>()
                .ForMember(dest => dest.BrandName , opt => opt.MapFrom(src => src.productBrand.Name))
                .ForMember(dest => dest.TypeName , opt => opt.MapFrom(src => src.productType.Name))
                .ForMember(dest => dest.PictureUrl , opt => opt.MapFrom(new PictureUrlResolver(configuration)));

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();

            CreateMap<BasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, Basket>().ReverseMap();
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Route.Domains.Entities.Products;
using Store.Route.Shared.DTOs.Products;

namespace Store.Route.Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(D => D.Brand, O => O.MapFrom(B => B.Brand.Name))
                .ForMember(D => D.Type, O => O.MapFrom(T => T.Type.Name))
                //.ForMember(D => D.PictureUrl, O => O.MapFrom(S => $"{configuration["BaseUrl"]}/{S.PictureUrl}"));
                .ForMember(D => D.PictureUrl, O => O.MapFrom(new ProductPictureUrlResolver(configuration)));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}

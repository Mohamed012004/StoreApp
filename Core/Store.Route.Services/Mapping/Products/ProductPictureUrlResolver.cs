using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Route.Shared.DTOs.Products;

namespace Store.Route.Services.Mapping.Products
{
    public class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}

using AdminDashBoard.Models;
using Store.Route.Domains.Entities.Products;
using Store.Route.Services.Specifications;

namespace AdminDashBoard.Specifications
{
    public class BrandSpecification : BaseSpecifications<int, ProductBrand>
    {
        public BrandSpecification(BrandAndTypeQueryParams queryParams) : base(
            B => string.IsNullOrEmpty(queryParams.SearchValue) || B.Name.ToLower() == queryParams.SearchValue.ToLower()
        )
        {
        }
    }
}
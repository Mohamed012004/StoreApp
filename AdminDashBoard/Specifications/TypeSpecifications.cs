using AdminDashBoard.Models;
using Store.Route.Domains.Entities.Products;
using Store.Route.Services.Specifications;

namespace AdminDashBoard.Specifications
{
    public class TypeSpecifications : BaseSpecifications<int, ProductType>
    {
        public TypeSpecifications(BrandAndTypeQueryParams queryParams) : base(
            T => string.IsNullOrEmpty(queryParams.SearchValue) || T.Name.ToLower() == queryParams.SearchValue.ToLower()
        )
        {
        }
    }
}
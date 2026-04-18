using Store.Route.Shared.DTOs.Products;

namespace Store.Route.Services.Specifications
{
    internal class ProductCountSpecification : BaseSpecifications<int, Product>
    {
        public ProductCountSpecification(ProductQueryParameters productParams) : base(

             P =>
                (!productParams.BrandId.HasValue || P.BrandId == productParams.BrandId)
                &&
                (!productParams.TypeId.HasValue || P.TypeId == productParams.TypeId)
                &&
                (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search.ToLower()))
            )
        {

        }
    }
}

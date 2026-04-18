using Store.Route.Shared.DTOs.Products;

namespace Store.Route.Services.Specifications
{
    partial class ProductWithBrandAndTypeSpecification : BaseSpecifications<int, Product>
    {
        public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
        {
            ApplyInclude();
        }
        // OrderBy
        // priceasc
        // pricedesc
        // name (default)
        public ProductWithBrandAndTypeSpecification(ProductQueryParameters productParams) : base
            (
                P =>
                (!productParams.BrandId.HasValue || P.BrandId == productParams.BrandId)
                &&
                (!productParams.TypeId.HasValue || P.TypeId == productParams.TypeId)
                &&
                (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search.ToLower()))
            )
        {
            ApplyPagination(productParams.PageIndex, productParams.PageSize);
            // ToDo.
            ApplySorting(productParams.Sort);
            ApplyInclude();
        }
        private void ApplyInclude()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }

        private void Apply()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }

        public void ApplySorting(string? sort)
        {

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        //OrderByDescending = P => P.Price;
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        //OrderBy = P => P.Name;
                        AddOrderBy(P => P.Name);
                        break;
                }

            }
            else
            {
                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);
            }
        }

    }


}

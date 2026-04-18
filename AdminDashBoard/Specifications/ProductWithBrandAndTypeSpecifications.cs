using Store.Route.Services.Specifications;

namespace AdminDashBoard.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
    {
        public ProductWithBrandAndTypeSpecifications() : base(P => true)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
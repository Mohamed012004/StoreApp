using Store.Route.Shared.DTOs.Products;

namespace Store.Route.Services.Abstractions.Products
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters productParams);

        Task<ProductResponse> GetProductsByIdAsync(int id);

        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();

    }
}

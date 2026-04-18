using AutoMapper;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Products;
using Store.Route.Domains.Exceptions.NotFound;
using Store.Route.Services.Abstractions.Products;
using Store.Route.Services.Specifications;
using Store.Route.Shared.DTOs.Products;

namespace Store.Route.Services.Products
{
    internal class ProductService(IUnitOfWork _unitofwork, IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters productParams)
        {
            var spec = new ProductWithBrandAndTypeSpecification(productParams);

            //var spec = new BaseSpecifications<int, Product>(null);
            //spec.Includes.Add(P => P.Brand);
            //spec.Includes.Add(P => P.Type);


            //var products = await _unitofwork.GetRepository<int, Product>().GetAllAsync(false);
            var products = await _unitofwork.GetRepository<int, Product>().GetAllAsync(spec, false);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var specCount = new ProductCountSpecification(productParams);
            var count = await _unitofwork.GetRepository<int, Product>().CountAsync(specCount);

            return new PaginationResponse<ProductResponse>(productParams.PageIndex, productParams.PageSize, count, result);
        }

        public async Task<ProductResponse> GetProductsByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);

            //var spec = new BaseSpecifications<int, Product>(P => P.Id == id);
            //spec.Includes.Add(P => P.Brand);
            //spec.Includes.Add(P => P.Type);

            //var product = await _unitofwork.GetRepository<int, Product>().GetAsync(id);
            var product = await _unitofwork.GetRepository<int, Product>().GetAsync(spec);
            if (product is null)
                throw new ProductNotFoundException(id);

            var productDto = _mapper.Map<ProductResponse>(product);
            return productDto;
        }


        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var brands = await _unitofwork.GetRepository<int, ProductBrand>().GetAllAsync(false);
            var brandsDto = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return brandsDto;
        }


        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var types = await _unitofwork.GetRepository<int, ProductType>().GetAllAsync(false);
            var typesDto = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return typesDto;
        }

    }
}

using AutoMapper;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Baskets;
using Store.Route.Domains.Exceptions.BadRequest;
using Store.Route.Domains.Exceptions.NotFound;
using Store.Route.Services.Abstractions.Baskets;
using Store.Route.Shared.DTOs.Baskets;

namespace Store.Route.Services.Baskets
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketasync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundException(id);
            var result = _mapper.Map<BasketDto>(basket);

            return result;
        }

        public async Task<BasketDto?> CreateBasketasync(BasketDto dto, TimeSpan duration)
        {
            var basket = _mapper.Map<CustomerBasket>(dto);
            var result = await _basketRepository.CreateBasketAsync(basket, duration);
            if (result is null) throw new CreateOrUpdateBadRequestException();

            return _mapper.Map<BasketDto>(result);
        }

        public async Task<bool> DeleteBasketasync(string id)
        {
            var flag = await _basketRepository.DeleteBaskerAsync(id);
            if (!flag) throw new DeleteBadRequstException();
            return flag;
        }


    }
}

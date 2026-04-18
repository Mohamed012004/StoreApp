using AutoMapper;
using Store.Route.Domains.Entities.Baskets;
using Store.Route.Shared.DTOs.Baskets;

namespace Store.Route.Services.Mapping.Baskets
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemsDto>().ReverseMap();
        }
    }
}

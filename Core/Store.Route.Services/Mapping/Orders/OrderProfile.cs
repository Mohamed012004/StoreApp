using AutoMapper;
using Store.Route.Domains.Entities.Order;
using Store.Route.Shared.DTOs.Orders;

namespace Store.Route.Services.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {


            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();

            CreateMap<Order, OrderResponse>()
                .ForMember(D => D.DeliveryMethod, Options => Options.MapFrom(S => S.DeleveryMethod.ShortName))
                .ForMember(D => D.Total, Options => Options.MapFrom(S => S.GetTotal()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(Dto => Dto.ProductId, Options => Options.MapFrom(S => S.Product.ProductId))
                .ForMember(Dto => Dto.ProductName, Options => Options.MapFrom(S => S.Product.ProductName))
                .ForMember(Dto => Dto.PictureUrl, Options => Options.MapFrom(S => S.Product.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodsResponse>();
        }
    }
}

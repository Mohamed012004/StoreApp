using AutoMapper;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Shared.DTOs.Auth;

namespace Store.Route.Services.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }

    }
}

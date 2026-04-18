using Store.Route.Shared.DTOs.Auth;

namespace Store.Route.Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserResponse?> LoginAsync(LoginRequest request);
        Task<UserResponse?> RegisterAsync(RegisterRequest request);

        Task<bool> CheckEmailExistsAsync(string email);
        Task<UserResponse?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);
        Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto dto, string email);

    }
}

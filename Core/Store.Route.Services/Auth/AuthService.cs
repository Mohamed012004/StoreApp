using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Domains.Exceptions.BadRequest;
using Store.Route.Domains.Exceptions.NotFound;
using Store.Route.Domains.Exceptions.UnAuthorized;
using Store.Route.Services.Abstractions.Auth;
using Store.Route.Shared;
using Store.Route.Shared.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Route.Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager, IOptions<JWTOptions> Options, IMapper _mapper) : IAuthService
    {
        public async Task<UserResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);
            var flag = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UnAuthorizedException();

            return new UserResponse()
            {
                DispayName = user.DisplayName,
                Email = request.Email,
                Tocken = await GenerateTockenAsync(user)
            };
        }

        public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                DisplayName = request.DisplayName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(E => E.Description).ToList());

            return new UserResponse()
            {
                DispayName = user.DisplayName,
                Email = request.Email,
                Tocken = await GenerateTockenAsync(user)
            };
        }



        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);

            return new UserResponse
            {
                DispayName = user.DisplayName,
                Email = user.Email,
                Tocken = await GenerateTockenAsync(user)
            };
        }
        public async Task<AddressDto?> GetCurrentUserAddressAsync(string email)
        {
            //_userManager.FindByEmailAsync(email); // This Function Don't Load Navigation Property

            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);
            return _mapper.Map<AddressDto>(user.Address);

        }

        public async Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto reques, string email)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);

            if (user.Address is null)
            {
                // Create New Address
                user.Address = _mapper.Map<Address>(reques);
            }
            else
            {
                // Update The Old Address
                user.Address.FirstName = reques.FirstName;
                user.Address.LastName = reques.LastName;
                user.Address.Street = reques.Street;
                user.Address.City = reques.City;
                user.Address.Country = reques.Country;

            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }


        private async Task<string> GenerateTockenAsync(AppUser user)
        {
            // TOCKEN:
            //1. Header     (Type , Algo)
            //2. PayLoad    (claims)
            //3. Signature  (Key)

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName , user.DisplayName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.MobilePhone , user.PhoneNumber)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var JwtOptions = Options.Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecurityKey));

            var tocken = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(JwtOptions.DurationDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(tocken);

        }






    }
}

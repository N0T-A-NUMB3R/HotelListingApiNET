using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto apiUserDto);
        Task<bool> Login(LoginDto loginDto);
    }
}
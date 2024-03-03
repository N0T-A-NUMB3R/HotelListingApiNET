using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Migrations;
using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto apiUserDto)
        {
            var user = _mapper.Map<ApiUser>(apiUserDto);
            user.UserName = apiUserDto.Email;

            var result = await _userManager.CreateAsync(user, apiUserDto.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                return default;
            }

            bool isValidCredentials = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isValidCredentials)
            {
                return default;
            }
            return isValidCredentials;
        }
    }
}

      

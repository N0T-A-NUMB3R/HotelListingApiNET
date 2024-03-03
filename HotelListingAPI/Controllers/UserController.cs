using HotelListingAPI.Contracts;
using HotelListingAPI.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<UserController> _logger;

        public UserController(IAuthManager authManager, ILogger<UserController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }
        //POST: api/User/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDTO)
        {
            _logger.LogInformation($"Registration attempt for {apiUserDTO.Email}");
           
            try
            {
                var errors = await _authManager.Register(apiUserDTO);
                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                return Ok();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Something wrong in the {nameof(Register)} for {apiUserDTO.Email}");
                return Problem($"Something wron in the {nameof(Register)}", statusCode: 500);
            }
           
        }

        //POST: api/User/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await  _authManager.Login(loginDto);
            if(authResponse is null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);    
        }
        //REFRESH TOKEN: api/user/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);
        }
    }
}

using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlogApi.Helpers;
using BlogApi.Repository.Interface;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AccountController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<TokenDto> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            return await _authService.RegisterUser(userRegisterDto);
        }

        [HttpPost("login")]
        public async Task<TokenDto> LoginUser([FromBody] LoginCredentialsDto userLoginDto)
        {
            return await _authService.LoginUser(userLoginDto);
        }

        [HttpPost("logout")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<IActionResult> LogoutUser()
        {
            Console.WriteLine(User.Identity.Name);
            var userId = Converter.GetId(HttpContext);
            return Ok($"User logged out successfully. With id: {userId}");
        }

        [HttpGet("profile")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = Converter.GetId(HttpContext);
            var userProfile = new UserDto
            {
            };

            return Ok(userProfile);
        }

        [HttpPut("profile")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserEditDto userProfileDto)
        {
            var userId = Converter.GetId(HttpContext);
            return Ok("User profile updated successfully.");
        }
    }
}
using System.Security.Claims;
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
            // Console.WriteLine(User.Identity.Name);
            var token = Converter.GetTokenFromContext(HttpContext);
            Console.WriteLine(token);
            await _authService.LogoutUser(token);
            return Ok($"User logged out successfully");
        }

        [HttpGet("profile")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<UserDto> GetProfile()
        {
            var userId = Converter.GetId(HttpContext);
            return await _userService.GetUserProfile(userId);
        }

        [HttpPut("profile")]
        [Authorize(Policy = "ValidateToken")]
        public async Task UpdateUserProfile([FromBody] UserEditDto userEditDto)
        {
            var id = Converter.GetId(HttpContext);
            await _userService.UpdateUserProfile(id, userEditDto);
        }
    }
}
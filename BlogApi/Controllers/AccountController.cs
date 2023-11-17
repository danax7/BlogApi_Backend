using BlogApi.DTO;
using BlogApi.DTO.AuthDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<TokenDto> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
           var token = new TokenDto
            {
                accessToken = "your_access_token",
            };

            return null;
        }

        [HttpPost("login")]
        public async Task<TokenDto> LoginUser([FromBody] UserDto userLoginDto)
        {
            var token = new TokenDto
            {
                accessToken = "your_access_token",
                // ex = 3600 //
            };

            return token;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            

            return Ok("User logged out successfully.");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
         

            var userProfile = new UserDto
            {
               
            };

            return Ok(userProfile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserEditDto userProfileDto)
        {
           

            return Ok("User profile updated successfully.");
        }
    }
}

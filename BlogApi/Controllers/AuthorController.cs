using BlogApi.DTO.AuthorDTO;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Services.Interface;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("list")]
        public async Task<List<AuthorDto>> GetAuthorList()
        {
            return await _authorService.GetAuthorList();
        }
    }
}
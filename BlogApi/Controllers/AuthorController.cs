using BlogApi.DTO.AuthorDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Entity.Enums;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        [HttpGet("list")]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthorList()
        {
              var authorList = new List<AuthorDto>
            {
                new AuthorDto
                {
                    fullName = "AgentLost",
                    birthDate  = new DateTime(1999, 2, 19),
                    gender = Gender.Female,
                    posts = 4,
                    likes = 0,
                    created = DateTime.Now
                },
                new AuthorDto
                {
                    fullName = "Xexi",
                    birthDate = new DateTime(1995, 1, 10),
                    gender = Gender.Female,
                    posts = 12,
                    likes = 0,
                    created = DateTime.Now
                },
                new AuthorDto
                {
                    fullName = "ЧеширКо",
                    birthDate = new DateTime(1990, 2, 15),
                    gender = Gender.Male,
                    posts = 1,
                    likes = 0,
                    created = DateTime.Now
                },
              
            };

            return Ok(authorList);
        }
    }
}
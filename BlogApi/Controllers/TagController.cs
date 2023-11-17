using BlogApi.DTO.TagDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> GetTagList()
        {
             var tagList = new List<TagDto>
            {
                new TagDto
                {
                    id = Guid.NewGuid(),
                    createTime = DateTime.Now,
                    name = "история"
                },
                new TagDto
                {
                    id = Guid.NewGuid(),
                    createTime = DateTime.Now,
                    name = "еда"
                },
                
            };

            return Ok(tagList);
        }
    }
}
using BlogApi.DTO.TagDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Services.Interface;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<List<TagDto>> GetTags()
        {
            var tags = await _tagService.GetTags();
            return tags;
        }
    }
}
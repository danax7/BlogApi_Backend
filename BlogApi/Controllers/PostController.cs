using BlogApi.DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.DTO;
using BlogApi.DTO.CommentDTO;
using BlogApi.DTO.TagDto;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Helpers;
using BlogApi.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize(Policy = "ValidateToken")]
        public async Task<PostPagedListDto> GetPosts(
            [FromQuery] Guid[] tags,
            [FromQuery] string? author,
            [FromQuery] int? minReadingTime,
            [FromQuery] int? maxReadingTime,
            [FromQuery] SortType sorting,
            [FromQuery] bool onlyMyCommunities,
            [FromQuery] int page = 1,
            [FromQuery] int size = 5)
        {
            var postFilterDto = new PostFilterDto
            {
                tags = tags,
                author = author,
                minReadingTime = minReadingTime,
                maxReadingTime = maxReadingTime,
                sorting = sorting,
                onlyMyCommunities = onlyMyCommunities,
                page = page,
                size = size
            };

            var userId = Converter.GetPossiblyNullableTokenId(HttpContext);
            if (userId == null)
            {
                postFilterDto.onlyMyCommunities = false;
            }

            return await _postService.GetPosts(postFilterDto, userId);
        }

        [HttpPost]
        [Authorize(Policy = "ValidateToken")]
        public async Task<ActionResult<Guid>> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var userId = Converter.GetId(HttpContext);
            var tagGuids = createPostDto.tags.Select(Guid.Parse).ToList();
            var postId = await _postService.CreatePost(createPostDto, tagGuids, userId);

            return Ok(postId);
        }

        [HttpGet("{id}")]
        public async Task<PostFullDto> GetPostById(Guid id)
        {
            return await _postService.GetPostById(id);
        }

        [HttpPost("{postId}/like")]
        public async Task<ActionResult> AddLike(Guid postId)
        {
            var userId = Converter.GetId(HttpContext);
            await _postService.AddLike(postId, userId);
            return Ok("Like added successfully");
        }

        [HttpDelete("{postId}/like")]
        public async Task<ActionResult> RemoveLike(Guid postId)
        {
            var userId = Converter.GetId(HttpContext);
            await _postService.RemoveLike(postId, userId);
            return Ok("Like removed successfully");
        }
    }
}
using BlogApi.DTO.CommunityDto;
using BlogApi.DTO.PostDTO;
using BlogApi.DTO.TagDto;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Helpers;
using BlogApi.Service.Interface;
using BlogApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/community")]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        private readonly IPostService _postService;

        public CommunityController(ICommunityService communityService, IPostService postService)
        {
            _communityService = communityService;
            _postService = postService;
        }

        [HttpGet("")]
        public async Task<List<CommunityDto>> GetCommunityList()
        {
            return await _communityService.GetCommunityList();
        }

        [HttpGet("my")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<List<CommunityUserDto>> GetMyCommunityList()
        {
            var userId = Converter.GetId(HttpContext);
            return await _communityService.GetMyCommunityList(userId);
        }

        [HttpGet("{id}")]
        public async Task<CommunityFullDto> GetCommunity(Guid id)
        {
            return await _communityService.GetCommunity(id);
        }

        [HttpPost("")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<IActionResult> CreateCommunity(CreateCommunityDto communityCreateDto)
        {
            var userId = Converter.GetId(HttpContext);
            await _communityService.CreateCommunity(userId, communityCreateDto);
            return Ok("Community created successfully");
        }

        //TODO:check
        [HttpGet("{id}/post")]
        [AllowAnonymous]
        [Authorize(Policy = "ValidateToken")]
        public async Task<PostPagedListDto> GetPosts(
            [FromRoute] Guid id,
            [FromQuery] Guid[] tags,
            [FromQuery] SortType sorting,
            [FromQuery] int page = 1,
            [FromQuery] int size = 5)
        {
            var postFilterDto = new PostFilterDto
            {
                tags = tags,
                sorting = sorting,
                page = page,
                size = size,
                communityId = id
            };

            var userId = Converter.GetPossiblyNullableTokenId(HttpContext);
            if (userId == null)
            {
                postFilterDto.onlyMyCommunities = false;
            }

            return await _postService.GetPosts(postFilterDto, userId);
        }

        [HttpPost("{id}/post")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<ActionResult> CreatePost(Guid id, [FromBody] CreatePostDto postCreateDto)
        {
            var userId = Converter.GetId(HttpContext);
            var tagGuids = postCreateDto.tags.Select(Guid.Parse).ToList();
            if (tagGuids.Count == 0)
            {
                return NotFound("Tags not found");
            }

            postCreateDto.communityId = id;
            await _postService.CreatePost(postCreateDto, tagGuids, userId);
            return Ok("Post created successfully");
        }

        [HttpGet("{id}/role")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<ActionResult<string>> GetGreatestUserCommunityRole(Guid id)
        {
            var userId = Converter.GetId(HttpContext);
            return await _communityService.GetGreatestUserCommunityRole(userId, id);
        }

        [HttpPost("{id}/subscribe")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<IActionResult> Subscribe(Guid id)
        {
            var userId = Converter.GetId(HttpContext);
            try
            {
                await _communityService.Subscribe(userId, id);
                return Ok("Successfully subscribed.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Failed to subscribe: {ex.Message}");
            }
        }

        [HttpDelete("{id}/unsubscribe")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<IActionResult> Unsubscribe(Guid id)
        {
            var userId = Converter.GetId(HttpContext);

            try
            {
                await _communityService.Unsubscribe(userId, id);
                return Ok("Successfully unsubscribed.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Failed to unsubscribe: {ex.Message}");
            }
        }
    }
}
using BlogApi.DTO.CommunityDto;
using BlogApi.DTO.PostDTO;
using BlogApi.DTO.TagDto;
using BlogApi.Entity;
using BlogApi.Entity.Enums;
using BlogApi.Helpers;
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

        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
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
            Console.WriteLine(userId);
            return await _communityService.GetMyCommunityList(userId);
        }

        [HttpGet("{id}")]
        public async Task<CommunityFullDto> GetCommunity(Guid id)
        {
            return await _communityService.GetCommunity(id);
        }

        [HttpGet("{id}/post")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<List<PostDto>> GetCommunityPostList(
            Guid id,
            [FromQuery] TagEntity[] tags,
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
            };

            //return await _communityService.GetCommunityPostList(id, postFilterDto);
            return null;
        }

        [HttpPost("{id}/post")]
        [Authorize(Policy = "ValidateToken")]
        public async Task CreatePost(Guid id, CreatePostDto postCreateDto)
        {
            // await _communityService.CreatePost(id, postCreateDto);
            Ok("Post created successfully");
        }

        [HttpGet("{id}/role")]
        [Authorize(Policy = "ValidateToken")]
        public async Task<CommunityRole> GetGreatestUserCommunityRole(Guid communityId)
        {
            var userId = Converter.GetId(HttpContext);
            return await _communityService.GetGreatestUserCommunityRole(userId, communityId);
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
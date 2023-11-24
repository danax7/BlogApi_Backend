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
using BlogApi.Service.Interface;

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
        public async Task<PostPagedListDto> GetPosts(
            [FromQuery] TagEntity[] tags,
            [FromQuery] string? author,
            [FromQuery] int? minReadingTime,
            [FromQuery] int? maxReadingTime,
            [FromQuery] SortType sorting,
            [FromQuery] bool onlyMyCommunities = false,
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

            return await _postService.GetPosts(postFilterDto);
        }

        // [HttpGet]
        // public async Task<ActionResult<PostDto>> GetPostList(
        //     [FromQuery] string[] tags,
        //     [FromQuery] string author,
        //     [FromQuery] int? minReadingTime,
        //     [FromQuery] int? maxReadingTime,
        //     [FromQuery] SortType sorting,
        //     [FromQuery] bool onlyMyCommunities = false,
        //     [FromQuery] int page = 1,
        //     [FromQuery] int size = 5)
        // {
        //  
        //     var posts = new List<PostDto>
        //     {
        //         new PostDto
        //         {
        //             id = Guid.NewGuid(),
        //             createTime = DateTime.Now,
        //             title = "Sample Post",
        //             description = "This is a sample post description.",
        //             readingTime = 5,
        //             image = "sample-image.jpg",
        //             authorId = Guid.NewGuid().ToString(),
        //             author = "Sample Author",
        //             communityId = Guid.NewGuid().ToString(),
        //             communityName = "Sample Community",
        //             addressId = Guid.NewGuid(),
        //             likes = 0,
        //             hasLike = false,
        //             commentsCount = 0,
        //             tags = new List<TagDto>
        //             {
        //                 new TagDto()
        //                 {
        //                     id = Guid.NewGuid(),
        //                     createTime = DateTime.Now,
        //                     name = "Sample Tag"
        //                 }
        //             }
        //         },
        //        
        //     };
        //
        //     
        //     var pagination = new PageInfoDto()
        //     {
        //         size = size,
        //         count = posts.Count,
        //         current = page
        //     };
        //     
        //     var response = new PostPagedListDto()
        //     {
        //         // posts = new List<PostDto>,
        //         pagination = pagination
        //     };
        //
        //     return Ok(response);
        // }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var postId = Guid.NewGuid();
            return Ok(postId);
        }

        [HttpGet("{id}")]
        public async Task<PostFullDto> GetPostById(Guid id)
        {
            return await _postService.GetPostById(id);
        }
        //[HttpGet("{id}")]
        // public async Task<ActionResult<PostFullDto>> GetPostDetail(Guid id)
        // {
        //       var postDetail = new PostFullDto
        //     {
        //         id = id,
        //         createTime = DateTime.Now,
        //         title = "Sample Post",
        //         description = "This is a sample post description.",
        //         readingTime = 5,
        //         image = "sample-image.jpg",
        //         authorId = Guid.NewGuid().ToString(),
        //         author = "Sample Author",
        //         communityId = Guid.NewGuid().ToString(),
        //         communityName = "Sample Community",
        //         addressId = Guid.NewGuid(),
        //         likes = 0,
        //         hasLike = false,
        //         commentsCount = 0,
        //         tags = new List<TagDto>
        //         {
        //             new TagDto
        //             {
        //                 id = Guid.NewGuid(),
        //                 createTime = DateTime.Now,
        //                 name = "история"
        //             },
        //             new TagDto
        //             {
        //                 id = Guid.NewGuid(),
        //                 createTime = DateTime.Now,
        //                 name = "еда"
        //             },
        //         
        //         },
        //         comments = new List<CommentEntity>()
        //         
        //     };
        //
        //     return Ok(postDetail);
        // }

        [HttpPost("{postId}/like")]
        public async Task<ActionResult> AddLike(Guid postId)
        {
            return Ok();
        }

        [HttpDelete("{postId}/like")]
        public async Task<ActionResult> RemoveLike(Guid postId)
        {
            return Ok();
        }
    }
}
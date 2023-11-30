using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.DTO.CommentDTO;
using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository
{
    public class CommentRepositoryImpl : ICommentRepository
    {
        private readonly BlogDbContext _context;

        public CommentRepositoryImpl(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommentDto>> GetAllFirstLevelCommentReplies(Guid comentId)
        {
            var comments = await _context.Comments
                .Where(comment => comment.ParentCommentId == comentId)
                .Include(comment => comment.User)
                .Include(comment => comment.Post)
                .OrderByDescending(comment => comment.createTime)
                .Select(comment => new CommentDto(comment))
                .ToListAsync();

            return comments;
        }


        public async Task<List<CommentDto>> GetAllFirstLevelPostCommentsById(Guid postId)
        {
            var comments = await _context.Comments
                .Where(comment => comment.PostId == postId && comment.ParentCommentId == null)
                .Include(comment => comment.User)
                .Include(comment => comment.Post)
                .OrderByDescending(comment => comment.createTime)
                .Select(comment => new CommentDto(comment))
                .ToListAsync();

            return comments;
        }


        // public async Task<List<CommentDto>> GetCommentTree(Guid postId)
        // {
        //     var comments = await _context.Comments
        //         .Where(comment => comment.PostId == postId && comment.ParentCommentId == null)
        //         .Include(comment => comment.User)
        //         .Include(comment => comment.Post)
        //         .Include(comment => comment.SubComments)
        //         .OrderByDescending(comment => comment.createTime)
        //         .Select(comment => new CommentDto(comment))
        //         .ToListAsync();
        //
        //     return comments;
        // }

        public async Task<List<CommentDto>> GetAllPostComments(Guid postId)
        {
            var comments = await _context.Comments
                .Where(comment => comment.PostId == postId)
                .Include(comment => comment.User)
                .OrderByDescending(comment => comment.createTime)
                .Select(comment => new CommentDto(comment))
                .ToListAsync();

            return comments;
        }

        public async Task CreateComment(CommentEntity comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(CommentEntity comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CommentEntity> GetCommentById(Guid id)
        {
            return await _context.Comments
                .Include(comment => comment.User)
                .Include(comment => comment.Post)
                .FirstOrDefaultAsync(comment => comment.id == id);
        }
    }
}
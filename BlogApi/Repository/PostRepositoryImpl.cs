using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class PostRepositoryImpl: IPostRepository
{
    private readonly BlogDbContext _context;
    
    public PostRepositoryImpl(BlogDbContext context)
    {
        _context = context;
    }
    
    public Task<PostEntity?> GetPostById(Guid id)
    {
        return _context.Posts.FirstOrDefaultAsync(dish => dish.id == id);
    }
    
}
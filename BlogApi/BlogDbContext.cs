using BlogApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi;

public class BlogDbContext : DbContext
{
    public DbSet<PostEntity> Posts { get; set; }
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

}
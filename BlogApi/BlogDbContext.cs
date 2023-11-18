using BlogApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi;

public class BlogDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<AccessTokenEntity> AccessTokens { get; set; }
    
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

}
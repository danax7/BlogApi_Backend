using BlogApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi;

public class BlogDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<AccessTokenEntity> BlackTokenList { get; set; }
    
    public DbSet<AuthorEntity> Authors { get; set; }
    
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<PostEntity>()
        //     .HasMany(p => p.tags)
        //     .WithMany(t => t.posts)
        //     .UsingEntity(j => j.ToTable("PostTag"));
        
        modelBuilder.Entity<TagEntity>(entity =>
        {
            entity.ToTable("Tags");
        });
        }
}
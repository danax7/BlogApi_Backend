using BlogApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi;

public class BlogDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommunityEntity> Communities { get; set; }
    public DbSet<UserCommunityEntity> UserCommunities { get; set; }
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

        modelBuilder.Entity<UserCommunityEntity>()
            .HasKey(uc => new { uc.UserId, uc.CommunityId });

        modelBuilder.Entity<UserCommunityEntity>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCommunities)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCommunityEntity>()
            .HasOne(uc => uc.Community)
            .WithMany(c => c.UserCommunities)
            .HasForeignKey(uc => uc.CommunityId);

        modelBuilder.Entity<TagEntity>(entity => { entity.ToTable("Tags"); });
    }
}
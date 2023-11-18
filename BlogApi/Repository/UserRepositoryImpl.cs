using BlogApi.Entity;
using BlogApi.Repository.Interface;

namespace BlogApi.Repository;

public class UserRepositoryImpl : IUserRepository
{
    private readonly BlogDbContext _blogContext;
    
    public UserRepositoryImpl(BlogDbContext blogContext)
    {
        _blogContext = blogContext;
    }
    

    public async Task CreateUser(UserEntity userEntity)
    {
        await _blogContext.Users.AddAsync(userEntity);
        await _blogContext.SaveChangesAsync();
    }
    
}
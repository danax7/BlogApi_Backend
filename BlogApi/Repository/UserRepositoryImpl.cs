using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<UserEntity?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _blogContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
    }
    
    public async Task<UserEntity?> GetUserByEmail(string email)
    {
        return await _blogContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }
    
}
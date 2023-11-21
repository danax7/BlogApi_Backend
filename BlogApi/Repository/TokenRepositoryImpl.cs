using BlogApi.Entity;
using BlogApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository;

public class TokenRepositoryImpl : ITokenRepository
{
    private readonly BlogDbContext _context;

    public TokenRepositoryImpl(BlogDbContext context)
    {
        _context = context;
    }

    public async Task CreateToken(AccessTokenEntity accessTokenEntity)
    {
        await _context.BlackTokenList.AddAsync(accessTokenEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<AccessTokenEntity?> GetToken(String token)
    {
        return await _context.BlackTokenList.FirstOrDefaultAsync(x => x.token == token);
    }
   
}
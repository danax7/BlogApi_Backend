using BlogApi.DTO.AuthorDTO;
using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IAuthorRepository
{
    public Task<List<AuthorEntity>> GetAuthorList();
    public Task<AuthorEntity> GetAuthorById(Guid authorId);
    public Task<AuthorEntity> GetAuthorByUserId(Guid userId);
    public Task CreateAuthor(AuthorEntity author);
    public Task UpdateAuthor(AuthorEntity author);
}
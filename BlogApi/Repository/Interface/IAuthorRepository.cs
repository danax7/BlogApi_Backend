using BlogApi.DTO.AuthorDTO;
using BlogApi.Entity;

namespace BlogApi.Repository.Interface;

public interface IAuthorRepository
{
    public Task<List<AuthorEntity>> GetAuthorList();
    public Task<AuthorEntity> GetAuthorById(Guid authorId);
    public Task CreateAuthor(AuthorEntity author);
}
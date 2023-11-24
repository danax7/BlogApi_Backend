using BlogApi.DTO.AuthorDTO;
using BlogApi.Entity;

namespace BlogApi.Services.Interface;

public interface IAuthorService
{
    public Task<List<AuthorDto>> GetAuthorList();
    public Task<AuthorEntity> GetAuthorById(Guid authorId);
    public Task CreateAuthor(Guid userId);
    //public Task<AuthorDto> UpdateAuthor(Guid authorId, AuthorUpdateDto authorUpdateDto);
}
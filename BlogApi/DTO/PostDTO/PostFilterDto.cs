using BlogApi.Entity.Enums;

namespace BlogApi.DTO.PostDTO;

public class PostFilterDto
{
    //TODO: Добавить теги, автора, время чтения, только мои группы
    public SortType? sorting { get; set; }
    public int page { get; set; }
    
}
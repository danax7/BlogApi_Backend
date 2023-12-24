namespace BlogApi.DTO.PostDTO;

public class PostPagedListDto
{
    public PostDto[] posts { get; set; }
    public PageInfoDto pagination { get; set; }
}
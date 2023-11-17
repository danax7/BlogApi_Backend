using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.PostDTO;

public class PostFilterDto
{
    public TagEntity[]? tags { get; set; }
    public string author { get; set; }
    public Int32? min { get; set; }
    public Int32? max { get; set; }
    public SortType? sorting { get; set; }
    public bool? onlyMyCommunities { get; set; }
    public int page { get; set; }
}
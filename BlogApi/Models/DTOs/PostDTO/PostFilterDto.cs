using BlogApi.Entity;
using BlogApi.Entity.Enums;

namespace BlogApi.DTO.PostDTO;

public class PostFilterDto
{
    public Guid[]? tags { get; set; }
    public string? author { get; set; }
    public Int32? minReadingTime { get; set; }
    public Int32? maxReadingTime { get; set; }
    public SortType? sorting { get; set; }
    public bool? onlyMyCommunities { get; set; }
    public int page { get; set; }

    public int size { get; set; }
}
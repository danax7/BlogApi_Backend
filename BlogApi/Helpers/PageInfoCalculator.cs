using BlogApi.DTO;

namespace BlogApi.Helpers;

public class PageInfoCalculator
{
    public static PageInfoDto GetPageInfoDto(int maxPageSize, int page, int PostCount)
    {
        var skipCount = (page - 1) * maxPageSize;
        var takeCount = Math.Min(maxPageSize, PostCount - skipCount);
        var pageCount = (int)Math.Ceiling((double)PostCount / maxPageSize);
        pageCount = pageCount == 0 ? 1 : pageCount;
        
        return new PageInfoDto
        {
            size = takeCount,
            count = pageCount,
            current = page
        };
    }
}
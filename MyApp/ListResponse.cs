namespace MyApp;

public class ListResponse<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<T> Items { get; set; }
    public ListResponse(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }
}
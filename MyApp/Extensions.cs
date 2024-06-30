using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
namespace MyApp;

public static class Extensions
{
    public static async Task<ListResponse<T>> ToPagedListAsync<T>(
    this IQueryable<T> source,
    int page,
    int pageSize)
    {
        var totalCount = await source.CountAsync();
        if (totalCount > 0)
        {
            var items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new ListResponse<T>(items, page, pageSize, totalCount);
        }
        return new(Enumerable.Empty<T>(), 0, 0, 0);
    }

    public static Uri AddQuery(this string path, IEnumerable<KeyValuePair<string, StringValues>> query)
    {
        var queryBuilder = new QueryBuilder(query);
        var uriBuilder = new UriBuilder()
        {
            Path = path,
            Query = queryBuilder.ToString()
        };
        return uriBuilder.Uri;
    }
}

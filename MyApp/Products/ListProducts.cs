using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
namespace MyApp.Products;
public static class ListProducts
{
    public class Request
    {
        public string? Name { get; set; }
    }

    public static async Task<RazorComponentResult> HandlePage(
        [FromServices] MyAppDbContext appDbContext,
        [AsParameters] Request request,
        HttpContext httpContext,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var name = request.Name ?? string.Empty;
        var results = await appDbContext.Set<Product>()
            .AsNoTracking().Where(p => p.Name.Contains(name) && p.DeletedAt == null).ToPagedListAsync(page, pageSize);
        var uri = "/products/list".AddQuery(new KeyValuePair<string, StringValues>[]
        {
            new("Name", request.Name),
            new("Page", page.ToString()),
            new("PageSize", pageSize.ToString())
        });
        httpContext.Response.Headers.Append("HX-Push-Url", uri.PathAndQuery);
        return new RazorComponentResult<ListProductsPage>(new { Results = results, Query = uri.Query });
    }
}

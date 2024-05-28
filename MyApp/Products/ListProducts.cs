using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MyApp.Products;
public static class ListProducts
{
    public class Request
    {
        public string? Name { get; set; }
    }

    public static async Task<RazorComponentResult> HandlePage([FromServices] MyAppDbContext appDbContext, [AsParameters] Request request)
    {
        var name = request.Name ?? string.Empty;
        var results = await appDbContext.Set<Product>().AsNoTracking().Where(p => p.Name.Contains(name)).ToListAsync();
        return new RazorComponentResult<ListProductsPage>(new { Results = results });
    }
}

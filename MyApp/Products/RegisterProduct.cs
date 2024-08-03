using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MyApp.Products;
public static class RegisterProduct
{
    public class Request
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
    }

    public static Task<RazorComponentResult> HandlePage()
    {
        return Task.FromResult<RazorComponentResult>(new RazorComponentResult<RegisterProductPage>());
    }

    public static async Task<RazorComponentResult> HandleAction(
        [FromServices] MyAppDbContext appDbContext,
        [FromBody] Request request,
        HttpContext httpContext)
    {
        var any = await appDbContext.Set<Product>().AsNoTracking().AnyAsync(p => p.Name == request.Name);
        if (any)
        {
            httpContext.Response.Headers.Append("HX-Reswap", $"outerHTML");
            httpContext.Response.Headers.Append("HX-Retarget", $"#product-form");
            return new RazorComponentResult<ProductForm>(new
            {
                Name = request.Name,
                NameErrorMessage = "The product already exists",
                Price = request.Price,
                Unit = request.Unit.ToString(),
                Description = request.Description
            });
        }
        var product = new Product()
        {
            ProductId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Unit = request.Unit,
            IsEnabled = false
        };
        appDbContext.Set<Product>().Add(product);
        await appDbContext.SaveChangesAsync();
        httpContext.Response.Headers.Append("HX-Trigger-After-Swap", @$"{{""successEvent"":""The product was register successfully""}}");
        return await ListProducts.HandlePage(appDbContext, new ListProducts.Request(), httpContext);
    }
}
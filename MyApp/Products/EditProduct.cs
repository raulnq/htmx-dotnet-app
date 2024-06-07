using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MyApp.Products;

public static class EditProduct
{
    public class Request
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
    }

    public static async Task<RazorComponentResult> HandlePage([FromRoute] Guid productId, [FromServices] MyAppDbContext appDbContext)
    {
        var product = await appDbContext.Set<Product>().AsNoTracking().FirstAsync(p => p.ProductId == productId);
        return new RazorComponentResult<EditProductPage>(new { Product = product });
    }

    public static async Task<RazorComponentResult> HandleAction(
        [FromRoute] Guid productId,
        [FromServices] MyAppDbContext appDbContext,
        [FromBody] Request request,
        HttpContext httpContext)
    {
        var product = await appDbContext.Set<Product>().FindAsync(productId);
        product.Description = request.Description;
        product.Price = request.Price;
        product.Unit = request.Unit;
        await appDbContext.SaveChangesAsync();
        httpContext.Response.Headers.Append("HX-Trigger-After-Swap", @$"{{""successEvent"":""The product was edited successfully""}}");
        return await ListProducts.HandlePage(appDbContext, new ListProducts.Request(), httpContext);
    }
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace MyApp.Products;

public static class EnableProduct
{
    public static async Task<RazorComponentResult> HandleAction(
        [FromRoute] Guid productId,
        [FromServices] MyAppDbContext appDbContext,
        HttpContext httpContext)
    {
        var product = await appDbContext.Set<Product>().FindAsync(productId);
        product.IsEnabled = true;
        await appDbContext.SaveChangesAsync();
        httpContext.Response.Headers.Append("HX-Trigger-After-Swap", @$"{{""successEvent"":""The product was enabled successfully""}}");
        return await EditProduct.HandlePage(productId, appDbContext);
    }
}
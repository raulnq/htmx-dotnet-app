using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace MyApp.Products;

public static class DeleteProduct
{
    public class Request
    {
        public string? Reason { get; set; }
    }

    public static Task<RazorComponentResult> HandlePage(
        [FromRoute] Guid productId,
        HttpContext context)
    {
        context.Response.Headers.Append("HX-Trigger-After-Swap", @"{""openModalEvent"":""true""}");
        return Task.FromResult<RazorComponentResult>(new RazorComponentResult<DeleteProductPage>(new
        {
            ProductId = productId,
        }));
    }

    public static async Task<RazorComponentResult> HandleAction(
        [FromRoute] Guid productId,
        [FromServices] MyAppDbContext appDbContext,
        [FromBody] Request request,
        HttpContext httpContext)
    {
        var product = await appDbContext.Set<Product>().FindAsync(productId);
        product.DeletedAt = DateTimeOffset.Now;
        product.DeletionReason = request.Reason;
        await appDbContext.SaveChangesAsync();

        httpContext.Response.Headers.Append("HX-Trigger-After-Swap", @$"{{""successEvent"":""The product was deleted successfully"", ""closeModalEvent"":""true""}}");
        return await ListProducts.HandlePage(appDbContext, new ListProducts.Request(), httpContext);
    }
}
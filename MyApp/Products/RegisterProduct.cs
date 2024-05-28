using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

    public static async Task<RazorComponentResult> HandleAction([FromServices] MyAppDbContext appDbContext, [FromBody] Request request)
    {
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
        return await ListProducts.HandlePage(appDbContext, new ListProducts.Request());
    }
}
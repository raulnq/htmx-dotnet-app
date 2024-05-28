namespace MyApp.Products;
public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsEnabled { get; set; }
    public Unit Unit { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletionReason { get; set; }
}
public enum Unit
{
    UN,
    KG
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace MyApp.Products;
public class EntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products");
        builder
            .HasKey(cr => cr.ProductId);
        builder
            .Property(c => c.Price)
            .HasColumnType("decimal(19, 4)");
        builder
            .Property(c => c.Unit)
            .HasConversion(s => s.ToString(), value => (Unit)Enum.Parse(typeof(Unit), value, true));
    }
}
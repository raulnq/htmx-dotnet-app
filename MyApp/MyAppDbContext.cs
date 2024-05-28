using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MyApp;

public class MyAppDbContext : DbContext
{
    public MyAppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyApp;
using MyApp.Products;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));
var app = builder.Build();
app.MapGet("/", () =>
{
    return new RazorComponentResult<MainPage>();
});
app.RegisterProductEndpoints();
app.Run();

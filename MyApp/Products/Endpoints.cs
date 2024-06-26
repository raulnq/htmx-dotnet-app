﻿namespace MyApp.Products;
public static class Endpoints
{
    public static void RegisterProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");
        group.MapGet("/list", ListProducts.HandlePage);
        group.MapGet("/register", RegisterProduct.HandlePage);
        group.MapPost("/register", RegisterProduct.HandleAction);
        group.MapGet("/{productId:guid}/edit", EditProduct.HandlePage);
        group.MapPost("/{productId:guid}/edit", EditProduct.HandleAction);
    }
}
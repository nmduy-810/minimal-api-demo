using Microsoft.EntityFrameworkCore;
using MinimalAPIsDemo.Data;
using MinimalAPIsDemo.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContextClass>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* Minimal Product API */

// Get the list of product
app.MapGet("/productlist", async (DbContextClass dbContext) =>
{
    var products = await dbContext.Product.ToListAsync();
    return Results.Ok(products);
});

// Get product by id
app.MapGet("/get-product-by-id", async (int id, DbContextClass dbContext) =>
{
    var product = await dbContext.Product.FindAsync(id);
    return product == null ? Results.NotFound() : Results.Ok(product);
});

app.MapPost("/create-product", async (Product product, DbContextClass dbContext) => 
{
    var result = dbContext.Product.Add(product);
    await dbContext.SaveChangesAsync();
    return Results.Ok(result.Entity);
});

// Update the product
app.MapPut("/update-product", async (Product product, DbContextClass dbContext) =>
{
    var productDetail = await dbContext.Product.FindAsync(product.ProductId);
    if (productDetail == null) 
        return Results.NotFound();
    
    productDetail.ProductName = product.ProductName;
    productDetail.ProductDescription = product.ProductDescription;
    productDetail.ProductPrice = product.ProductPrice;
    productDetail.ProductStock = product.ProductStock;

    await dbContext.SaveChangesAsync();
    return Results.Ok(productDetail);
});

// Delete the product by id
app.MapDelete("/delete-product/{id}", async (int id, DbContextClass dbContext) =>
{
    var product = await dbContext.Product.FindAsync(id);
    if (product == null)
    {
        return Results.NoContent();
    }
    dbContext.Product.Remove(product);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
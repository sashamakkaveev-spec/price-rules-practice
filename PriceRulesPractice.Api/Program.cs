var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>
{
    new Product(1, "Молоко", 110m),
    new Product(2, "Хлеб", 60m)
};

app.MapGet("/", () => "Hello World!");

app.MapGet("/products/{id:int}", (int id) =>
{
    Product? product = products.Find(p => p.Id == id);
    if (product is null)
    {
        return Results.NotFound(new
        {
            message = "Product not found"
        });
    }

    return Results.Ok(product);
});

app.MapGet("/products", (decimal? minPrice, decimal? maxPrice) =>
{
    if (minPrice < 0m)
    {
        return Results.BadRequest(new
        {
            message = "Minimum price can't be negative"
        });
    }

    if (maxPrice < 0m)
    {
        return Results.BadRequest(new
        {
            message = "Maximum price can't be negative"
        });
    }

    if (minPrice > maxPrice)
    {
        return Results.BadRequest(new
        {
            message = "Minimum price can't be bigger than maximum price"
        });
    }

    IEnumerable<Product> filteredProducts = products;

    if (minPrice is not null)
    {
        filteredProducts = filteredProducts
            .Where(p => p.Price >= minPrice.Value);
    }

    if (maxPrice is not null)
    {
        filteredProducts = filteredProducts
            .Where(p => p.Price <= maxPrice.Value);
    }
    
    return Results.Ok(filteredProducts.ToList());
});
    
app.Run();
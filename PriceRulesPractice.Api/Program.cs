var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>
{
    new Product(1, "Молоко", 110m),
    new Product(2, "Хлеб", 60m)
};

var productFilter = new ProductFilter();

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
    try
    {
        var filteredProducts = productFilter.Filter(
            products,
            minPrice,
            maxPrice
        );

        return Results.Ok(filteredProducts);
    }
    catch (ArgumentException exception)
        when (exception.ParamName is nameof(minPrice) or nameof(maxPrice))
    {
        return Results.BadRequest(new
        {
            message = exception.Message
        });
    }
});
    
app.Run();
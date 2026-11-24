public class ProductFilter
{
    public List<Product> Filter(
        IEnumerable<Product> products,
        decimal? minPrice,
        decimal? maxPrice)
    {
        ArgumentNullException.ThrowIfNull(products);
        
        if (minPrice < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minPrice),
                minPrice,
                "Minimum price can't be negative"
            );
        }

        if (maxPrice < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxPrice),
                maxPrice,
                "Maximum price can't be negative"
            );
        }

        if (minPrice > maxPrice)
        {
            throw new ArgumentException(
                "Minimum price can't be bigger than maximum price",
                nameof(minPrice)
            );
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
        
        return filteredProducts.ToList();
    }
}
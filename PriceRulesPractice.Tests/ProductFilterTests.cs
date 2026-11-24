using NUnit.Framework;

public class ProductFilterTests
{
    private static readonly int[] ExpectedIds = [1, 2, 3]; // Для теста Filter_NoBounds_ReturnsAllProducts
    
    [Test]
    public void Filter_MinAndMax_ReturnsOnlyProductsInRange()
    {
        var products = new List<Product>
        {
            new Product(1, "Молоко", 110m),
            new Product(2, "Хлеб", 60m),
            new Product(3, "Сыр", 350m)
        };
        
        var filter = new ProductFilter();
        var result = filter.Filter(products, 100m, 200m);
        
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(1));
    }
    
    [Test]
    public void Filter_EqualBounds_IncludesProductOnBoundary()
    {
        var products = new List<Product>
        {
            new Product(1, "Молоко", 110m),
            new Product(2, "Хлеб", 60m),
            new Product(3, "Сыр", 350m)
        };
        
        var filter = new ProductFilter();
        var result = filter.Filter(products, 60m, 60m);
        
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(2));
    }

    [Test]
    public void Filter_NegativeMaxPrice_ThrowsForMaxPrice()
    {
        List<Product> products = [];
        var filter = new ProductFilter();
        
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => filter.Filter(products, null, -1m)
        )!;

        Assert.Multiple(() =>
        {
            Assert.That(exception.ParamName, Is.EqualTo("maxPrice"));
            Assert.That(exception.ActualValue, Is.EqualTo(-1m));
        });
    }

    [Test]
    public void Filter_NegativeMinPrice_ThrowsForMinPrice()
    {
        List<Product> products = [];
        var filter = new ProductFilter();
        
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => filter.Filter(products, -1m, null)
        )!;

        Assert.Multiple(() =>
        {
            Assert.That(exception.ParamName, Is.EqualTo("minPrice"));
            Assert.That(exception.ActualValue, Is.EqualTo(-1m));
        });
    }

    [Test]
    public void Filter_MinGreaterThanMax_ThrowsArgumentException()
    {
        List<Product> products = [];
        var filter = new ProductFilter();
        
        var exception = Assert.Throws<ArgumentException>(
            () => filter.Filter(products, 200m, 100m)
        )!;
        
        Assert.That(exception.ParamName, Is.EqualTo("minPrice"));
    }

    [Test]
    public void Filter_NullProducts_ThrowsArgumentNullException()
    {
        var filter = new ProductFilter();
        
        var exception = Assert.Throws<ArgumentNullException>(
            () => filter.Filter(null!, null, null)
        )!;
        
        Assert.That(exception.ParamName, Is.EqualTo("products"));
    }
    
    [Test]
    public void Filter_NoBounds_ReturnsAllProducts()
    {
        var products = new List<Product>
        {
            new Product(1, "Молоко", 110m),
            new Product(2, "Хлеб", 60m),
            new Product(3, "Сыр", 350m)
        };
        
        var filter = new ProductFilter();
        var result = filter.Filter(products, null, null);
        
        Assert.That(
            result.Select(p => p.Id),
            Is.EquivalentTo(ExpectedIds)
        );
    }

    [Test]
    public void Filter_NoMatches_ReturnsEmptyList()
    {
        var products = new List<Product>
        {
            new Product(1, "Молоко", 110m),
            new Product(2, "Хлеб", 60m),
            new Product(3, "Сыр", 350m)
        };
        
        var filter = new ProductFilter();
        var result = filter.Filter(products, 70m, 100m);
        
        Assert.That(result, Is.Empty);
    }
}
using NUnit.Framework;

public class FixedDiscountRuleTests
{
    [Test]
    public void Apply_DiscountExceedsPrice_ReturnsZero()
    {
        var rule = new FixedDiscountRule(200m);
        decimal result = rule.Apply(100m);
        Assert.That(result, Is.EqualTo(0m));
    }
    
    [Test]
    public void Apply_DiscountLessThanPrice_ReturnsReducedPrice()
    {
        var rule = new FixedDiscountRule(200m);
        decimal result = rule.Apply(1000m);
        Assert.That(result, Is.EqualTo(800m));
    }
    
    [Test]
    public void Apply_NegativePrice_ThrowsArgumentOutOfRangeException()
    {
        var rule = new FixedDiscountRule(200m);
        Assert.Throws<ArgumentOutOfRangeException>(
            () => rule.Apply(-1m)
        );
    }

    [Test]
    public void Constructor_NegativeDiscount_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new FixedDiscountRule(-1m)
        );
    }
}
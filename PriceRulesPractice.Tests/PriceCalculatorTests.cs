using NUnit.Framework;

public class PriceCalculatorTests
{
    [Test]
    public void Calculate_DiscountThenMinimum_Returns950()
    {
        IPriceRule[] rules =
        [
            new PercentageDiscountRule(10m),
            new MinimumPriceRule(950m),
        ];
        
        var calculator = new PriceCalculator(rules);
        decimal result = calculator.Calculate(1000m);
        
        Assert.That(result, Is.EqualTo(950m));
    }
    
    [Test]
    public void Calculate_MinimumThenDiscount_Returns810()
    {
        IPriceRule[] rules =
        [
            new MinimumPriceRule(950m),
            new PercentageDiscountRule(10m),
        ];
        
        var calculator = new PriceCalculator(rules);
        decimal result = calculator.Calculate(1000m);
        
        Assert.That(result, Is.EqualTo(900m));
    }
    
    [Test]
    public void Calculate_NoRules_ReturnsOriginalPrice()
    {
        IPriceRule[] rules = [];
        
        var calculator = new PriceCalculator(rules);
        decimal result = calculator.Calculate(1000m);
        
        Assert.That(result, Is.EqualTo(1000m));
    }

    [Test]
    public void Calculate_NegativePriceWithNoRules_ThrowsArgumentOutOfRangeException()
    {
        IPriceRule[] rules = [];
        
        var calculator = new PriceCalculator(rules);
        Assert.Throws<ArgumentOutOfRangeException>(
            () => calculator.Calculate(-1m)
        );
    }
}
IPriceRule[] rules =
{
    new PercentageDiscountRule(10m),
    new FixedDiscountRule(100m),
    new MinimumPriceRule(850m),
};

var calculator = new PriceCalculator(rules);

Console.WriteLine(calculator.Calculate(1000m));

public interface IPriceRule
{
    decimal Apply(decimal price);
}

public class PercentageDiscountRule : IPriceRule
{
    private readonly decimal _percent;
    
    public PercentageDiscountRule(decimal percent)
    {
        if (percent < 0m || percent > 100m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(percent),
                percent,
                "Percentage discount must be between 0 and 100"
            );
        }
        _percent = percent;
    }
    
    public decimal Apply(decimal price)
    {
        if (price < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                price,
                "Price can't be negative"
            );
        }

        var discount = price * _percent / 100m;
        return price - discount;
    }
}

public class MinimumPriceRule : IPriceRule
{
    private readonly decimal _minimumPrice;
    
    public MinimumPriceRule(decimal minimumPrice)
    {
        if (minimumPrice < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumPrice),
                minimumPrice,
                "Minimum price can't be negative"
            );
        }
        _minimumPrice = minimumPrice;
    }

    public decimal Apply(decimal price)
    {
        if (price < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                price,
                "Price can't be negative"
            );
        }
        return Math.Max(price, _minimumPrice);
    }
}

public class FixedDiscountRule : IPriceRule
{
    private readonly decimal _fixedDiscount;

    public FixedDiscountRule(decimal fixedDiscount)
    {
        if (fixedDiscount < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(fixedDiscount),
                fixedDiscount,
                "Fixed discount can't be negative"
            );
        }
        _fixedDiscount = fixedDiscount;
    }

    public decimal Apply(decimal price)
    {
        if (price < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                price,
                "Price can't be negative"
            );
        }
        return Math.Max(price - _fixedDiscount, 0m);
    }
}

public class PriceCalculator(IPriceRule[] rules)
{
    private readonly IPriceRule[] _rules = rules ?? throw new ArgumentNullException(nameof(rules), "Rules can't be null");

    public decimal Calculate(decimal price)
    {
        if (price < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                price,
                "Price can't be negative"
            );
        }

        foreach (var rule in _rules)
        {
            price = rule.Apply(price);
        }
        return price;
    }
}
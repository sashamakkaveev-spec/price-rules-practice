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
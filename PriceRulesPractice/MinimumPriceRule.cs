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
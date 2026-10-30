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
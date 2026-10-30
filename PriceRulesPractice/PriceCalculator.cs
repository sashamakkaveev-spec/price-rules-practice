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
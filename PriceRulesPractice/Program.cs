IPriceRule[] rules =
{
    new PercentageDiscountRule(10m),
    new FixedDiscountRule(100m),
    new MinimumPriceRule(850m),
};

var calculator = new PriceCalculator(rules);

Console.WriteLine(calculator.Calculate(1000m));
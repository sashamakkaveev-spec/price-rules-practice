public interface IPriceRule
{
    decimal Apply(decimal price);
}
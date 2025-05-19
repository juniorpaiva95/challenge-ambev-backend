namespace Ambev.DeveloperEvaluation.Domain.Common;

public static class DiscountCalculator
{
    public static decimal CalculateDiscount(int quantity)
    {
        if (quantity < 4)
            return 0m;
        if (quantity >= 10 && quantity <= 20)
            return 0.20m;
        if (quantity >= 4)
            return 0.10m;
        return 0m;
    }
} 
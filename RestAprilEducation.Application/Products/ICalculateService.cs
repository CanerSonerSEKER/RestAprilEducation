namespace RestAprilEducation.Application.Products
{
    public interface ICalculateService
    {
        decimal CalculatePriceWithTaxes(decimal price, decimal taxRate);

    }
}
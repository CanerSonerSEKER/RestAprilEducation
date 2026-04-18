using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Application.Products
{
    public class CalculateService : ICalculateService
    {
        public decimal CalculatePriceWithTaxes(decimal price, decimal taxRate)
        {
            return price * taxRate;
        }
    }
}

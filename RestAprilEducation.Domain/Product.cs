using RestAprilEducation.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;


        // field burada price değişkenini tutar, property ise bu değişkene erişimi kontrol eder.
        public decimal Price
        {
            get => field;
            set
            {
                if (value < 1 || value > 1000)
                {
                    throw new BusinessException("Price must be between 1 to 1000")
                    {
                        ErrorDetail = $"Invalid price: {value}"
                    };
                }
                field = value;
            }

        }

        //public void SetPrice(decimal price)
        //{
        //    if (price < 1 || price > 1000)
        //    {
        //        throw new Exception("Price must be between 1 to 1000");
        //    }
        //    Price = price;
        //}


        public string Barcode { get; set; } = null!; 

    }
}

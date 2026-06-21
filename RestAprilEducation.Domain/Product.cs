using RestAprilEducation.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RestAprilEducation.Domain
{

    // object = data +behaviour => rich domain model / anemic domain model 
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;


        // field burada price değişkenini tutar, property ise bu değişkene erişimi kontrol eder.
        public decimal Price { get; private set; }
        //{ Burada da validasyon yapılabiliyor diye göstermek için yaptık.
        //    get => field;
        //    set
        //    {
        //        if (value < 1 || value > 1000)
        //        {
        //            throw new BusinessException("Price must be between 1 to 1000")
        //            {
        //                ErrorDetail = $"Invalid price: {value}"
        //            };
        //        }
        //        field = value;
        //    }

        //}

        //public void SetPrice(decimal price)
        //{
        //    if (price < 1 || price > 1000)
        //    {
        //        throw new Exception("Price must be between 1 to 1000");
        //    }
        //    Price = price;
        //}


        public string Barcode { get; set; } = null!;


        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public void SetPrice(decimal price) 
        {
            if (price < 0) 
            {
                throw new Exception("Fiyat alanı sıfırdan küçük olamaz");
            }

            Price = price;
        }



    }
}

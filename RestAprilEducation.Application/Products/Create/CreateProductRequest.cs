using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestAprilEducation.Application.Products.Create
{
    public record CreateProductRequest(
        //[Required(ErrorMessage = "Name is required")]
        string? Name, 
        //[Range(1,1000, ErrorMessage = "Price must be between 1 and 100")]
        //[Required]
        decimal? Price);
}

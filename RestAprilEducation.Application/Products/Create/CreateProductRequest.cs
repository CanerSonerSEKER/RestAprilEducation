using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Application.Products.Create
{
    public record CreateProductRequest(string Name, decimal Price);
}

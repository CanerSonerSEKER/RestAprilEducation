using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Domain
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Bir kategorinin birden fazla product ' ı olabilir
        public List<Product>? Products { get; set; }

    }
}

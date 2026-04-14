using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Application.Products.GetList
{

    /// <summary>
    /// 
    /// Bu sınıf, ürünlerin listelenmesi işlemi sırasında kullanılacak veri transfer nesnesini temsil eder.
    /// Ve bu sınıf, ürünlerin temel bilgilerini içeren bir yapıya sahiptir. Bu bilgiler arasında ürünün benzersiz kimliği (Id), adı (Name) ve fiyatı (Price) bulunmaktadır.
    /// Bu yapıyı ise Primary Constructor (birincil yapıcı) kullanarak tanımlanmıştır. Bu sayede, bu sınıfın bir örneği oluşturulurken, bu üç parametrenin sağlanması gerekmektedir.
    /// 
    /// </summary>
    public record ProductDto(int Id, string Name, decimal Price);

    // var p = new ProductDto();
    // p.Id = 1; -- Kullanılamaz bir yapı. Çünkü içerisinde set edilebilir propertyler yok.




}

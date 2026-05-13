using RestAprilEducation.Application;
using System.Net;

namespace RestAprilEducation.API.Extensions
{
    public static class EndpointExtensions
    {
        // Created => response body boş bırakılarbilir. Header da ise getini yazmamız lazım. /api/products/1 gibi 



        // Extension method to convert ApplicationResult to IResult
        // Bu extension method, ApplicationResult nesnesini IResult türüne dönüştürmek için kullanılabilir.
        // Bu sayede, API endpoint'lerinde ApplicationResult döndüren servisler kolayca IResult formatına çevrilebilir ve HTTP yanıtları oluşturulabilir.
        public static IResult ToResult(this ApplicationResult applicationResult)
        {
            if (applicationResult.IsSuccess)
            {

                return applicationResult switch
                {
                    { HttpStatusCode: HttpStatusCode.Created } => Results.Created(string.Empty, null),
                    { HttpStatusCode: HttpStatusCode.NoContent } => Results.NoContent()
                };

            }

            return Results.Problem(applicationResult.Problem!);
        }



        public static IResult ToResult<T>(this ApplicationResult<T> applicationResult)
        {

            if (applicationResult.IsSuccess)
            {
                return applicationResult switch
                {
                    { HttpStatusCode: HttpStatusCode.Created } => Results.Created(string.Empty, applicationResult.Data),
                    _ => Results.Ok(applicationResult.Data)
                };


            }

            return Results.Problem(applicationResult.Problem!);

        }

    }
}

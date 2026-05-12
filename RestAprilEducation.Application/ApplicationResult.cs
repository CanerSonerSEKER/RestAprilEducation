using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;

namespace RestAprilEducation.Application
{
    public class ApplicationResult
    {

        // Update ve Delete kısmında geriye bir şey dönmek zorunda değiliz. 
        // Success  => No Content => 204
        // Success => Content => 200 Ok / 201 Created
        // Failure => No Content => Not Found => 404
        // Failure => Error Content => 400 Bad Request/ 500 / Internal Server Error

        // Object tüm tiplerin ana sınıfı olarak düşünülebilir.
        public ProblemDetails? Problem;

        public bool IsSuccess => Problem == null;

        // 200 OK
        // 201 Created
        // 204 No Content
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }



        public static ApplicationResult  Success()
        {
            return new ApplicationResult();
        }

        public static ApplicationResult Failure(ProblemDetails problem, HttpStatusCode status)
        {
            return new ApplicationResult()
            {
                Problem = problem,
                HttpStatusCode = status
            };
        }

        public static ApplicationResult Failure(string title, HttpStatusCode status)
        {
            return new ApplicationResult()
            {
                Problem = new ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                },

                HttpStatusCode = status
            }; 
        }


    }

    // Eğer bir şey dönmem gerekiyorsa bu sınıfı kullanabilirim. Eğer bir şey dönmeyecekse ApplicationResult sınıfını kullanabilirim.
    public class ApplicationResult<T> : ApplicationResult
    {
        public T? Data { get; set; }

        public new static ApplicationResult<T> Success(T data, HttpStatusCode status)
        {
            return new ApplicationResult<T>()
            {
                Data = data,
                HttpStatusCode = status
            };
        }

        public new static ApplicationResult<T> Failure(ProblemDetails problem, HttpStatusCode status )
        {
            return new ApplicationResult<T>()
            {
                Problem = problem,
                HttpStatusCode = status
            };
        }

        public new static ApplicationResult<T> Failure(string title, HttpStatusCode status)
        {
            return new ApplicationResult<T>()
            {
                Problem = new ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                },
                HttpStatusCode = status
            };
        }


    }   

}

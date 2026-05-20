using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Domain.Exceptions;
using System.Net;

namespace RestAprilEducation.API.ExceptionHandlers
{
    public class BusinessExceptionHandler : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            if (exception is not BusinessException domainException)
                return false;

            ProblemDetails problemDetails = new ProblemDetails();

            problemDetails.Status = HttpStatusCode.BadRequest.GetHashCode();
            problemDetails.Title = exception.Message;

            if(string.IsNullOrEmpty(domainException.ErrorDetail))
                problemDetails.Detail = domainException.ErrorDetail;
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }





    }
}

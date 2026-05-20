using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RestAprilEducation.API.ExceptionHandlers
{
    public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(
                exception, "Exception occured: {Message}", exception.Message);


            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "Unexpected error occurred",
                Status = StatusCodes.Status500InternalServerError
            };


            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }


    }
}

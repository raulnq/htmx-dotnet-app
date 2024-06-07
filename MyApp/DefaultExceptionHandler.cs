using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace MyApp;

public class DefaultExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public DefaultExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is InvalidOperationException ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                {
                    Title = "An error occurred",
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "application-error",
                },
                Exception = exception
            });
        }
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
                {
                    Title = "An error occurred",
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "internal-server-error",
                },
            Exception = exception
        });
    }
}
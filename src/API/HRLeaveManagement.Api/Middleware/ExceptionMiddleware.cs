using System.Net;
using HRLeaveManagement.Api.Models;
using src.Core.Exceptions;

namespace HRLeaveManagement.Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        dynamic problem = ex switch
        {
            BadRequestException badRequestException => new CustomValidationProblemDetails
            {
                Title = badRequestException.Message,
                Status = (int)statusCode,
                Detail = badRequestException.InnerException?.Message,
                Type = nameof(BadRequestException),
                Errors = badRequestException.ValidationErrors
            },
            NotFoundException notFound => new CustomValidationProblemDetails
            {
                Title = notFound.Message,
                Status = (int)statusCode,
                Detail = notFound.InnerException?.Message,
                Type = nameof(NotFoundException),
            },
            _ => new CustomValidationProblemDetails
            {
                Title = ex.Message,
                Status = (int)statusCode,
                Detail = ex.StackTrace,
                Type = nameof(HttpStatusCode.InternalServerError),
            },
        };
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync((CustomValidationProblemDetails)problem);
    }
}
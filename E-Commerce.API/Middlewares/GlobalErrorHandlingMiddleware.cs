using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> _logger,RequestDelegate _next)
    {

        public async Task InvokeAsync(HttpContext httpContext)//httpContext Carry my current request
        {
            try
            {
                await _next(httpContext);//hatkhod el request beta3y teb3ato ll next middleware
                if (httpContext.Response.StatusCode==(int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundEndPointAsync(httpContext);
                }

            }

            catch (Exception exception)
            {
                _logger.LogError($"Something went wrong {exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response =new ErrorDetails
            {
                StatusCode=(int)HttpStatusCode.NotFound,  
                ErrorMessage=$"The End Point {httpContext.Request.Path} Not Found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            // set default status code to 500
            httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            // set content type application / json
            httpContext.Response.ContentType="application/json";

            var response = new ErrorDetails
            {
                ErrorMessage = exception.Message
            };

            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                ValidationException validationException=> HandleValidationException (validationException , response),
                _ => (int)HttpStatusCode.InternalServerError
               
            };

            // return standard response
            response.StatusCode = httpContext.Response.StatusCode;
         

            await httpContext.Response.WriteAsync(response.ToString());


        }

        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
            response.Errors=validationException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}

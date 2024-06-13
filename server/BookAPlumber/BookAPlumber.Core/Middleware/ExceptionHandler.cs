using BookAPlumber.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BookAPlumber.Core.Middleware
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;
        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ExceptionResponse exceptionResponse = new ExceptionResponse();
            var response = httpContext.Response;
            response.ContentType = "application/json";

            var errorId = Guid.NewGuid();
            
            exceptionResponse.Id = errorId;

            logger.LogError(ex, $"{errorId} : {ex.Message}");

            switch (ex)
            {
                case ApplicationException:
                    exceptionResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    exceptionResponse.StatusMessage = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case BadRequestException:
                    exceptionResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    exceptionResponse.StatusMessage = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    exceptionResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                    exceptionResponse.StatusMessage = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case NotFoundException:
                    exceptionResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    exceptionResponse.StatusMessage = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case DuplicateException:
                    exceptionResponse.StatusCode = (int)HttpStatusCode.Conflict;
                    exceptionResponse.StatusMessage = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                default:
                    exceptionResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionResponse.StatusMessage = "Internal Server Error, Please retry after a few minutes.";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(exceptionResponse));
        }
    }
}

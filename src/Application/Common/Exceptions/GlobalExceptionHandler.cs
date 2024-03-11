using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using System;

namespace ChequeMicroservice.Application.Common.Exceptions
{
   public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            _logger.LogError(
                context.Exception, "Exception occurred: {Message}", context.Exception);
            throw new NotImplementedException();
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,  Exception exception)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);
            httpContext.Response.ContentType = "text/plain";
            httpContext.Response.StatusCode = 501;
            await httpContext.Response.WriteAsync($"It don't work: {exception.Message}");
            return true;
        }
    }
}
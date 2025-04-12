using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SH.APIProblemDetails
{
    /// <summary>
    /// 全局异常处理器 ProblemDetailsException
    /// 实现API 异常处理的提示功能
    /// </summary>
    public class ProblemDetailsExceptionHandler : IExceptionHandler
    {

        private readonly IProblemDetailsService problemDetailsService;

        public ProblemDetailsExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            this.problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ProblemDetailsException problemDetailsException)
            {
                return true;
            }
            var problemDetails = new ProblemDetails()
            {
                Title = problemDetailsException.Error,
                Status = StatusCodes.Status400BadRequest,
                Detail = problemDetailsException.Message,
                Type = "Bad Request",
            };
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
            });

        }
    }
}

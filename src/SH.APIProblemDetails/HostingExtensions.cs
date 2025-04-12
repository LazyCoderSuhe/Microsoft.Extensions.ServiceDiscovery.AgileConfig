using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SH.APIProblemDetails
{
    public static class HostingExtensions
    {
        /// <summary>
        /// 添加全局异常处理器
        /// 在 app 中启动全局异常处理器
        /// // Configure the HTTP request pipeline.  app.UseExceptionHandler();

        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddProblemDetailsExceptionHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<ProblemDetailsExceptionHandler>();
            services.AddProblemDetailsFor9457();
            return services;
        }
        public static IServiceCollection AddProblemDetailsFor9457(this IServiceCollection services, int statusCode = StatusCodes.Status400BadRequest)
        {
            services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context => {
                    context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                    context.ProblemDetails.Status = statusCode;
                    context.HttpContext.Response.StatusCode = statusCode;
                    var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    if (activity != null)
                    {
                        context.ProblemDetails.Extensions.TryAdd("traceId", activity.TraceId.ToString());
                        context.ProblemDetails.Extensions.TryAdd("spanId", activity.SpanId.ToString());
                    }
                };
            });
            return services;
        }
     
    }
}

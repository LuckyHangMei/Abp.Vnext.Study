using Hsl_Blog.ToolKits.Base;
using Hsl_Blog.ToolKits.Extensions;
using System.Net;

namespace Hsl_Blog.HttpApi.Hosting.Middleware
{
    /// <summary>
    /// 异常中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        /// <summary>
        /// RequestDelegate是一种请求委托类型，用来处理HTTP请求的函数，返回的是delegate，
        /// </summary>
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next=next;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(httpContext, ex.Message);
            }
            finally
            {
                var statusCode=httpContext.Response.StatusCode;
                if (statusCode != StatusCodes.Status200OK)
                {
                    Enum.TryParse(typeof(HttpStatusCode), statusCode.ToString(), out object message);
                    await ExceptionHandlerAsync(httpContext, message.ToString());
                }
            }
        }
        /// <summary>
        /// 异常处理，返回JSON
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ExceptionHandlerAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
            var result = new ServiceResult();
            result.IsFailed(message);
            await context.Response.WriteAsync(result.ToJson());
        }
    }
}

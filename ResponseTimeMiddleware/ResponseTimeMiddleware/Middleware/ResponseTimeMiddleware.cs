using System.Diagnostics;

namespace ResponseTimeMiddleware.Middleware
{
    public class ResponseTimeMiddleware
    {
        const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time";
        readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next) => _next = next;

       public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                watch.Stop();
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = watch.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });
            return this._next(context);
        }
    }


  
}

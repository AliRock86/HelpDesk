namespace HelpDesk.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            if (path.HasValue && path.Value.StartsWith("/Home"))
            {
                if(httpContext.Session.GetString("UserName") == null)
                {
                    httpContext.Response.Redirect("/Auth/Login");
                }
            }

            return _next(httpContext);
        }

    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}

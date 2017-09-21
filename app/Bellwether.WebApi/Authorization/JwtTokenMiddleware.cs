using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Bellwether.WebApi.Authorization
{
    public class JwtTokenMiddleware
    {
       private readonly RequestDelegate _next;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly JwtOptions _options;

        public JwtTokenMiddleware(
            RequestDelegate next,
            JwtTokenGenerator jwtTokenGenerator,
            IOptions<JwtOptions> options
            )
        {
            _next = next;
            _jwtTokenGenerator = jwtTokenGenerator;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (!IsRequestForToken(context))
            {
                return _next(context);
            }

            if (!IsVialidTokenRequest(context))
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request");
            }

            var userEmail = GetUserEmail(context);
            var password = GetUserPassword(context);

            bool rememberMe;
            var rememberMeForm = context.Request.Form["rememberMe"].FirstOrDefault();
            bool.TryParse(rememberMeForm, out rememberMe);

            return _jwtTokenGenerator.GenerateToken(context, userEmail, password, rememberMe);
        }

        private static bool IsVialidTokenRequest(HttpContext context)
        {
            return context.Request.Method.Equals("POST") && context.Request.HasFormContentType;
        }

        private bool IsRequestForToken(HttpContext context)
        {
            return context.Request.Path.Equals(_options.Path, StringComparison.Ordinal);
        }

        private StringValues GetUserPassword(HttpContext context)
        {
            return context.Request.Form["password"];
        }

        private StringValues GetUserEmail(HttpContext context)
        {
            return context.Request.Form["email"];
        }
    }
}

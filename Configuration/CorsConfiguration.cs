using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace searchpe_exchange_rate
{
    public class CorsConfiguration
    {

        private readonly RequestDelegate _next;

        public CorsConfiguration(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            // Added "Accept-Encoding" to this list
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
            // New Code Starts here
            if (httpContext.Request.Method == "OPTIONS")
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await httpContext.Response.WriteAsync(string.Empty);
            }
            // New Code Ends here
            await _next(httpContext);
        }
    }
}

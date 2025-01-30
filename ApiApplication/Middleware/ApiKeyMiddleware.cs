using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApiApplication.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER_NAME = "X-Api-Key";

        //This api Key should not be static but specific to each client of this API. But for simplicity we are using a static key here.
        private const string API_KEY = "1234abcd"; 

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey) || extractedApiKey != API_KEY)
            {
                context.Response.StatusCode = 401; 
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            await _next(context);
        }
    }
}

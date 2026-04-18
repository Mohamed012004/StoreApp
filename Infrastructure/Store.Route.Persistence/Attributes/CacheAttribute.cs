using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Route.Services.Abstractions;
using System.Text;

namespace Store.Route.Persistence.Attributes
{
    public class CacheAttribute(int TimeInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // logic
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            // Generate Key
            var cacheKey = GetCacheKey(context.HttpContext.Request);

            var result = await cacheService.GetAsync(cacheKey);
            if (!string.IsNullOrEmpty(result))
            {
                //context.HttpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";
                //context.HttpContext.Response.Headers["Access-Control-Allow-Headers"] = "*";
                //context.HttpContext.Response.Headers["Access-Control-Allow-Methods"] = "*";

                var response = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = response;
                return;
            }


            var actionContext = await next.Invoke();
            if (actionContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(TimeInSec));
            }
        }

        private string GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");
            }

            return key.ToString();
        }
    }
}

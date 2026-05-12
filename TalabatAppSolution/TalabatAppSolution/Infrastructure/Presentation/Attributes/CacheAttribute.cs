using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstractionLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int Duration = 150) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheKey = CreateCacheKey(context.HttpContext.Request);

            var CacheServices = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();

            var CasheValue = await CacheServices.GetAsync(CacheKey);

            if(CasheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CasheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }

            var ExecutedContext = await next.Invoke();

            if(ExecutedContext.Result is ObjectResult result)
            {
                await CacheServices.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(Duration));
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();

            key.Append(request.Path + '?');

            foreach(var item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}

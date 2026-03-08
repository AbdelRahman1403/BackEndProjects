using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace TalabatApp.CustomMiddelwares
{
    public class CustomExeceptionMiddelwares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExeceptionMiddelwares> _logger;

        public CustomExeceptionMiddelwares(RequestDelegate Next , ILogger<CustomExeceptionMiddelwares> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    throw new NotFoundException($"The {context.Request.Path} is not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.ContentType = "application/json";

                var errorToReturn = new ErrorToReturn
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(errorToReturn);
            }
        }
    }
}

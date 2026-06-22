using ApplicationLayer.Exceptions;
using ApplicationLayer.Exceptions;
using Shared.ErrorModels;

namespace HealthCareSystem.CustomMiddelwares
{
    public class CustomExeceptionMiddelwares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExeceptionMiddelwares> _logger;

        public CustomExeceptionMiddelwares(RequestDelegate Next, ILogger<CustomExeceptionMiddelwares> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    throw new NotFoundException(
                        $"The endpoint '{context.Request.Path}' was not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var errorToReturn = new ErrorToReturn
                {
                    Message = ex.Message
                };

                if (ex is AppException appException)
                {
                    context.Response.StatusCode = appException.StatusCode;
                }
                else
                {
                    context.Response.StatusCode =
                        StatusCodes.Status500InternalServerError;

                    errorToReturn.Message =
                        "An unexpected error occurred.";
                }

                //if (ex is BadRequestException badRequestException)
                //{
                //    errorToReturn.Errors = badRequestException.Errors?.ToList();
                //}

                errorToReturn.StatusCode =
                    context.Response.StatusCode;

                context.Response.ContentType =
                    "application/json";

                await context.Response.WriteAsJsonAsync(errorToReturn);
            }
        }
    }
}

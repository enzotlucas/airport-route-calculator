namespace Airport.RouteCalculator.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, 
                                      ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RouteNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);

                await HandleExceptionAsync(context, ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex.Message);

                await HandleExceptionAsync(context, ex);
            }
            catch (InfrastructureException ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);

                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Ocorreu um erro inesperado.", ex);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, RouteNotFoundException exception)
        {
            var code = HttpStatusCode.NotFound;

            return ErrorResponse(context, exception, code);
        }

        private static Task HandleExceptionAsync(HttpContext context, BusinessException exception)
        {
            var code = HttpStatusCode.BadRequest;

            return ErrorResponse(context, exception, code);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            return ErrorResponse(context, exception, code);
        }

        private static Task ErrorResponse(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}

using BuildingBlocks.Common.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace HealthCare.API.Middlewares
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(
            HttpContext context,
            Exception exception)
        {
            var problem = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            if (exception is AppException appEx)
            {
                problem.Status = appEx.StatusCode;
                problem.Title = ReasonPhrases.GetReasonPhrase(appEx.StatusCode);
                problem.Detail = appEx.Message;
            }
            else
            {
                problem.Status = 500;
                problem.Title = "Internal Server Error";
                problem.Detail = "An unexpected error occurred";
            }

            context.Response.StatusCode = problem.Status.Value;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}

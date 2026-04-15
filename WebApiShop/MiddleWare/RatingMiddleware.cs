using Enteties;
using Services;

namespace WebApiShop.MiddleWare
{
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;
        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService, ILogger<RatingMiddleware> logger)
        {
            try
            {
                Rating rating = new Rating();
                rating.Host = httpContext.Request.Host.Value;
                rating.Method = httpContext.Request.Method;
                rating.Path = httpContext.Request.Path;
                rating.Referer = httpContext.Request.Headers.Referer;
                rating.UserAgent = httpContext.Request.Headers.UserAgent;
                rating.RecordDate = DateTime.Now;
                await ratingService.AddRating(rating);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to record rating; continuing without blocking request.");
            }
            await _next(httpContext);
        }
    }

    public static class RatingExtensions
    {
        public static IApplicationBuilder UseRating(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}

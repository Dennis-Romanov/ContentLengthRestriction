using System.Threading.Tasks;
using ContentRestriction.Configurations.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ContentRestriction.Configurations.Middleware
{
    public class ContentLengthRestrictionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ContentLengthRestrictionMiddleware> _logger;
        private readonly ContentLengthRestrictionOptions _contentLengthRestrictionOptions;

        public ContentLengthRestrictionMiddleware(
            RequestDelegate requestDelegate,
            ILoggerFactory loggerFactory,
            ContentLengthRestrictionOptions contentLengthRestrictionOptions)
        {
            _requestDelegate = requestDelegate;
            _logger = loggerFactory.CreateLogger<ContentLengthRestrictionMiddleware>();

            _contentLengthRestrictionOptions = contentLengthRestrictionOptions;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (_contentLengthRestrictionOptions != null
                && _contentLengthRestrictionOptions.ContentLengthLimit > 0
                && httpContext.Request.ContentLength.HasValue
                && httpContext.Request.ContentLength.Value > _contentLengthRestrictionOptions.ContentLengthLimit)
            {
                _logger.LogWarning("Rejecting request with Content-Length {0} more than allowed {1}.",
                    httpContext.Request.ContentLength,
                    _contentLengthRestrictionOptions.ContentLengthLimit);

                httpContext.Response.StatusCode = StatusCodes.Status413RequestEntityTooLarge;

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Title = "Request too large",
                    Status = StatusCodes.Status413RequestEntityTooLarge,
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.11"
                });

                await httpContext.Response.CompleteAsync();
            }
            else
            {
                await _requestDelegate.Invoke(httpContext);
            }
        }
    }
}
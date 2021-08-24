using ContentRestriction.Configurations.Middleware;
using ContentRestriction.Configurations.Options;
using Microsoft.AspNetCore.Builder;

namespace ContentRestriction.Configurations.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseContentLengthRestriction(
            this IApplicationBuilder builder,
            ContentLengthRestrictionOptions contentLengthRestrictionOptions)
        {
            return builder.UseMiddleware<ContentLengthRestrictionMiddleware>(
                contentLengthRestrictionOptions);
        }
    }
}
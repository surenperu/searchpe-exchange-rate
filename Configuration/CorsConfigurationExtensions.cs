using Microsoft.AspNetCore.Builder;

namespace searchpe_exchange_rate
{
    public static class CorsConfigurationExtensions
    {
        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsConfiguration>();
        }
    }
}

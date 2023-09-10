using AspNetCoreRateLimit;

namespace SecureId.AccessControl.API.Extensions
{
    public static class RateLimitServiceExtension
    {
        public static IServiceCollection AddRateLimitService(this IServiceCollection services, IConfiguration config)
        {
            services.AddMemoryCache();
            //var rateconfig = _config.GetOptions<IpRateLimitOptions>("IpRateLimiting")
            var rateconfig = config.GetSection("IpRateLimiting");
            services.Configure<IpRateLimitOptions>(rateconfig);
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();
            return services;
        }
    }
}

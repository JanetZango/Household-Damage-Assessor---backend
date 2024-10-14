using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace ACM.Helpers.Localization
{
    public static class SessionLocalizationServiceCollectionExtensions
    {
        public static void AddSessionLocalization(this IServiceCollection services)
        {
            services.AddTransient<IStringLocalizerFactory, SessionStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }
    }
}

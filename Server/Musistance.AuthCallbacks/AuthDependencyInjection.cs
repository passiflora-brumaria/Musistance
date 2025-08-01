using Musistance.AuthCallbacks.Itch.Implementations;
using Musistance.AuthCallbacks.Itch.Interfaces;
using Musistance.AuthCallbacks.Itch.Settings;

namespace Musistance.AuthCallbacks
{
    public static class AuthDependencyInjection
    {
        /// <summary>
        /// Adds integration to itch.io for OAuth.
        /// </summary>
        /// <param name="services">Dependency injection container.</param>
        /// <param name="config">App configuration.</param>
        /// <returns>The dependency injection container, so that calls may be chained.</returns>
        public static IServiceCollection AddItch (this IServiceCollection services, IConfiguration config)
        {
            ItchSettings settings = config.GetSection("ItchOAuth").Get<ItchSettings>();
            return services.AddSingleton<ItchSettings>(settings).AddTransient<IItchIntegration,ItchIntegration>();
        }
    }
}

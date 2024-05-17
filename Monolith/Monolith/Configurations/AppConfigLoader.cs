using Domain.Common;

namespace Monolith.Configurations
{
    public static class AppConfigLoader
    {
        public static void SettingsBinding(this IConfiguration configuration)
        {
            do
            {
                AppConfig.ConnectionStrings = new ConnectionStrings();
                AppConfig.FirebaseConfig = new FirebaseConfig();
                AppConfig.JwtSetting = new JwtSetting();
                AppConfig.CorsConfig = new CorsConfig();
                AppConfig.RedisConfig = new RedisConfig();
                AppConfig.SwaggerConfig = new SwaggerConfig();
                AppConfig.SentryConfig = new SentryConfig();
            } while (AppConfig.ConnectionStrings == null);

            configuration.Bind("ConnectionStrings", AppConfig.ConnectionStrings);
            //configuration.Bind("FirebaseConfig", AppConfig.FirebaseConfig);
            //configuration.Bind("JwtSetting", AppConfig.JwtSetting);
            //configuration.Bind("CorsConfig", AppConfig.CorsConfig);
            //configuration.Bind("RedisConfig", AppConfig.RedisConfig);
            //configuration.Bind("SwaggerConfig", AppConfig.SwaggerConfig);
            //configuration.Bind("Sentry", AppConfig.SentryConfig);
        }
    }
}

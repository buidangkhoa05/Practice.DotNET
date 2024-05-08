namespace Domain.Common
{
    public class AppConfig
    {
        public static bool IsDevelopmentEnvironment { get; set; } = false;
        public static ConnectionStrings ConnectionStrings
        { get; set; }
        public static FirebaseConfig FirebaseConfig { get; set; }
        public static JwtSetting JwtSetting { get; set; }
        public static CorsConfig CorsConfig { get; set; }
        public static RedisConfig RedisConfig { get; set; }
        public static SwaggerConfig SwaggerConfig { get; set; }
        public static SentryConfig SentryConfig { get; set; }
    }

    public class FirebaseConfig
    {
        public string DefaultPath { get; set; } = null!;
        public string BucketName { get; set; } = null!;
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class JwtSetting
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; } = true;
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; } = true;
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }

    public class CorsConfig
    {
        public string Origins { get; set; } = null!;
    }

    public class RedisConfig
    {
        public string DefaultConnection { get; set; } = null!;
    }

    public class SwaggerConfig
    {
        public string Title { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TermsOfService { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string ContactUrl { get; set; } = null!;
        public string LicenseName { get; set; } = null!;
        public string LicenseUrl { get; set; } = null!;
        public string EndpointUrl { get; set; } = null!;
        public string EndpointDescription { get; set; } = null!;
        public string RoutePrefix { get; set; } = null!;
        public string AuthorizationHeaderKey { get; set; } = null!;
    }

    public class SentryConfig
    {
        public string Dsn { get; set; } = null!;
    }

}

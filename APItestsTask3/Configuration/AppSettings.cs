using Microsoft.Extensions.Configuration;

namespace APItestsTask3.Configuration
{
    public class AppSettings
    {
        private static readonly Lazy<AppSettings> _instance = new(() => new AppSettings());

        private AppSettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            DbConnection = configuration.GetValue<string>("DbConnection")!;
            WebApi = configuration.GetSection("WebApi").Get<WebApiSettings>()!;

        }

        public static AppSettings Current => _instance.Value;

        public string DbConnection { get; }

        public WebApiSettings WebApi { get; }
    }
}
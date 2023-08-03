using Microsoft.Extensions.Configuration;

namespace UItests.Configuration
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

            Browser = configuration.GetValue<string>("Browser")!;
            BaseUrl = configuration.GetValue<string>("BaseUrl")!;
            User = configuration.GetSection("User").Get<UserSettings>()!;
        }

        public static AppSettings Current => _instance.Value;

        public string Browser { get; }

        public string BaseUrl { get; }

        public UserSettings User { get; }
    }
}
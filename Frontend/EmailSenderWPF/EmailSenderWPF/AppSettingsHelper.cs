using System.Configuration;

namespace EmailSenderWPF
{
    public class AppSettingsHelper
    {
        public static string? GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}


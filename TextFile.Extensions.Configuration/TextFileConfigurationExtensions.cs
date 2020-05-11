using TextFile.Extensions.Configuration;

namespace Microsoft.Extensions.Configuration
{
    public static class TextFileConfigurationExtensions
    {
        public const char DefaultSeparator = ':';

        public static IConfigurationBuilder AddTextFileConfiguration(this IConfigurationBuilder builder, string path)
        {
            return AddTextFileConfiguration(builder, path, DefaultSeparator);
        }

        public static IConfigurationBuilder AddTextFileConfiguration(this IConfigurationBuilder builder, string path, char separator)
        {
            return builder.Add(new TextFileConfigurationSource(path, separator));
        }
    }
}
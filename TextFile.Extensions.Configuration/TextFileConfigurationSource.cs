using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace TextFile.Extensions.Configuration
{
    public class TextFileConfigurationSource : IConfigurationSource
    {
        public string Path { get; }

        public char Separator { get; }

        public TextFileConfigurationSource(string path, char separator)
        {
            Path = path;
            Separator = separator;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureValidPath(Path);
            return new TextFileConfigurationProvider(this);
        }

        public void EnsureValidPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or white space");
            }
        }
    }
}

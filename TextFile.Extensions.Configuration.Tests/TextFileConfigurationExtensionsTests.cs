using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NFluent;
using Xunit;

namespace TextFile.Extensions.Configuration.Tests
{
    public class TextFileConfigurationExtensionsTests
    {
        public const char DefaultSeparator = ':';
        public const string DefaultPath = "testFile.txt";

        [Theory]
        [InlineData(null)]
        [InlineData("     ")]
        [InlineData("")]
        public void Should_Throw_On_InvalidPath(string path)
        {
            var builder = new ConfigurationBuilder();
            builder.AddTextFileConfiguration(path);

            Check.ThatCode(() => builder.Build()).ThrowsType(typeof(ArgumentException));
        }

        [Fact]
        public void Should_Add_TextFileConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddTextFileConfiguration(DefaultPath);

            Check.That(builder.Sources).Not.IsEmpty();
            Check.That(builder.Sources.First()).IsInstanceOf<TextFileConfigurationSource>();

            var textFileSource = (TextFileConfigurationSource)builder.Sources.First();
            Check.That(textFileSource.Path).Equals(DefaultPath);
            Check.That(textFileSource.Separator).Equals(DefaultSeparator);
        }

        [Theory]
        [InlineData('|')]
        [InlineData('.')]
        [InlineData(';')]
        public void Should_Add_TextFileConfiguration_With_Separator(char separator)
        {
            var builder = new ConfigurationBuilder();

            builder.AddTextFileConfiguration(DefaultPath, separator);

            Check.That(builder.Sources).Not.IsEmpty();
            Check.That(builder.Sources.First()).IsInstanceOf<TextFileConfigurationSource>();

            var textFileSource = (TextFileConfigurationSource)builder.Sources.First();
            Check.That(textFileSource.Path).Equals(DefaultPath);
            Check.That(textFileSource.Separator).Equals(separator);
        }
    }
}
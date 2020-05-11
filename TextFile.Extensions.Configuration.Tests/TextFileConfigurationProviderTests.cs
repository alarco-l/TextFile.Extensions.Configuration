using System;
using System.IO;
using NFluent;
using Xunit;

namespace TextFile.Extensions.Configuration.Tests
{
    public class TextFileConfigurationProviderTests
    {
        public const char DefaultSeparator = ':';

        [Theory]
        [InlineData(null)]
        [InlineData("     ")]
        [InlineData("")]
        public void Should_Throw_On_InvalidPath(string path)
        {
            var configurationSource = new TextFileConfigurationSource(path, DefaultSeparator);
            var configurationProvider = new TextFileConfigurationProvider(configurationSource);

            Check.ThatCode(() => configurationProvider.Load()).ThrowsType(typeof(ArgumentException));
        }

        [Fact]
        public void Should_Throw_On_FileNotFound()
        {
            var configurationSource = new TextFileConfigurationSource("missing.txt", DefaultSeparator);
            var configurationProvider = new TextFileConfigurationProvider(configurationSource);

            Check.ThatCode(() => configurationProvider.Load()).ThrowsType(typeof(FileNotFoundException));
        }

        [Theory]
        [InlineData("toto", "tata")]
        [InlineData("foo", "bar")]
        [InlineData("toto.tata", "tutu")]
        [InlineData("titi", "toto.tata")]
        [InlineData("SomeKey", "SomeValue")]
        [InlineData("Some_Key", "Some_Value")]
        public void Should_Load_Configuration_From_File(string key, string expectedValue)
        {
            var configurationSource = new TextFileConfigurationSource("testFile.txt", DefaultSeparator);
            var configurationProvider = new TextFileConfigurationProvider(configurationSource);

            configurationProvider.Load();
            configurationProvider.TryGet(key, out var value);

            Check.That(value).Equals(expectedValue);
        }

        [Theory]
        [InlineData("toto", "tata:tutu")]
        [InlineData("titi:toto", "tata")]
        public void Should_Handle_Custom_Separator(string key, string expectedValue)
        {
            var configurationSource = new TextFileConfigurationSource("testFile.txt", '.');
            var configurationProvider = new TextFileConfigurationProvider(configurationSource);

            configurationProvider.Load();
            configurationProvider.TryGet(key, out var value);

            Check.That(value).Equals(expectedValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TextFile.Extensions.Configuration
{
    public class TextFileConfigurationProvider : ConfigurationProvider
    {
        private readonly TextFileConfigurationSource _configurationSource;

        public TextFileConfigurationProvider(TextFileConfigurationSource configurationSource)
        {
            _configurationSource = configurationSource;
        }

        public override void Load()
        {
            _configurationSource.EnsureValidPath(_configurationSource.Path);

            string[] stringData = Array.Empty<string>();

            try
            {
                stringData = File.ReadAllLines(_configurationSource.Path);
            }
            catch (Exception)
            {
                throw new FileNotFoundException($"File does not exist: {_configurationSource.Path}");
            }

            var data = new Dictionary<string, string>(stringData.Length / 2);

            for (var i = 0; i < stringData.Length; i++)
            {
                var kvp = stringData[i].Split(new char[]{_configurationSource.Separator});

                if (kvp.Length < 2)
                {
                    continue;
                }

                data.Add(kvp[0], kvp[1]);
            }

            Data = data;
        }
    }
}
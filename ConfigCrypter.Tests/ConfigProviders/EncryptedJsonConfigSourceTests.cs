﻿using System.Collections.Generic;
using DevAttic.ConfigCrypter.Extensions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DevAttic.ConfigCrypter.Tests.ConfigProviders
{
    public class EncryptedJsonConfigSourceTests
    {
        [Fact]
        public void AddEncryptedAppSettings_DecryptsValuesOnTheFly()
        {
            var certLoaderMock = Mocks.CertificateLoader;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddEncryptedAppSettings(config =>
            {
                config.KeysToDecrypt = new List<string> { "Test:ToBeEncrypted" };
                config.CertificateLoader = certLoaderMock.Object;
            });
            var configuration = configBuilder.Build();

            var decryptedValue = configuration["Test:ToBeEncrypted"];

            Assert.Equal("This is going to be encrypted", decryptedValue);
        }

        [Fact]
        public void AddEncryptedJsonConfig_DecryptsValuesOnTheFly()
        {
            var certLoaderMock = Mocks.CertificateLoader;
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddEncryptedJsonConfig(config =>
            {
                config.KeysToDecrypt = new List<string> { "KeyToEncrypt" };
                config.CertificateLoader = certLoaderMock.Object;
                config.Path = "config.json";
            });
            var configuration = configBuilder.Build();

            var decryptedValue = configuration["KeyToEncrypt"];

            Assert.Equal("This will be encrypted.", decryptedValue);
        }
    }
}

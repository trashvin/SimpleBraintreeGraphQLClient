using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BraintreeGraphQLConnector;
using BraintreeGraphQLConnector.Interface;

namespace ConnectorUnitTest
{
    [TestClass]
    public class ConnectorTest
    {
        
        [TestMethod]
        public void TestInitialization()
        {
            TestSetting setting = new TestSetting
            {
                Url = "http://test.com",
                PrivateKey = "ABC",
                PublicKey = "123",
                Version = "1"
            };

            Connector testConnector = new Connector(setting);

            Assert.AreEqual(setting.Url, testConnector.GetSettings().Url);
            Assert.AreEqual(setting.PublicKey, testConnector.GetSettings().PublicKey);
            Assert.AreEqual(setting.Version, testConnector.GetSettings().Version);
        }
    }

    internal class TestSetting : IAppSettings
    {
        public string Url { get; set; }

        public string PrivateKey { get; set; }

        public string PublicKey { get; set; }
        public string Version { get; set; }
    }
}

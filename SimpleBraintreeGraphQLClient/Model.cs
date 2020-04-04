using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using BraintreeGraphQLConnector.Interface;


namespace SimpleBraintreeGraphQLClient
{
    internal class AppSetting : IAppSettings
    {
        public string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["Url"];
            }
            set { }
        }
        public string PrivateKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PrivateKey"];
            }
            set { }
        }
        public string PublicKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicKey"];
            }
            set { }
        }
        public string Version
        {
            get
            {
                return ConfigurationManager.AppSettings["Version"];
            }
            set { }
        }
    }
}

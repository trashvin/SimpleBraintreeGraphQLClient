using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SimpleBraintreeGraphQLClient
{
    public class BraintreeQuery
    {
        // lower caps was used to fit with the graphql api requirement
        public string query { get; set; }
    }

    internal class AppSetting
    {
        public string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["Url"];
            }
        }
        public string PrivateKey 
        {
            get
            {
                return ConfigurationManager.AppSettings["PrivateKey"];
            } 
        }
        public string PublickKey
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicKey"];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using BraintreeGraphQLConnector.Interface;

namespace BraintreeGraphQLConnector
{
    public class BraintreeQuery
    {
        private string _schemaString = String.Empty;
        private Constants.QUERY_FORM _queryForm = Constants.QUERY_FORM.QUERY;
        private string _description = String.Empty;
        public BraintreeQuery
        (
            Constants.QUERY_FORM queryForm,
            string schemaName,
            string queryDescription = ""
        )
        {
            _schemaString = schemaName;
            _queryForm = queryForm;
            _description = queryDescription;
        }

        // lower caps was used to fit with the graphql api requirement
        public string query
        {
            get
            {
                StringBuilder query = new StringBuilder();

                if (this._queryForm == Constants.QUERY_FORM.QUERY)
                {
                    query.Append("query{");
                }
                else
                {
                    query.Append("mutation{");
                }
                query.Append($"{this._schemaString}}}");

                return query.ToString();
            }
        }

    }

    internal class BraintreeSubSchema
    {
        public BraintreeSubSchema(string name)
        {

        }
    }

    public class AppSettings: IAppSettings
    {
        public string Url { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string Version { get; set; }
    }
}
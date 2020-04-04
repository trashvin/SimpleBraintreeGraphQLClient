using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BraintreeGraphQLConnector.Interface;

namespace BraintreeGraphQLConnector
{
    public class Connector : IDisposable
    {
        private IAppSettings _config;
        public Connector(IAppSettings config)
        {
            _config = config;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IAppSettings GetSettings()
        {
            return _config;
        }




    }
}

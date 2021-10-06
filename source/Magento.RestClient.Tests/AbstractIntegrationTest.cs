using Magento.RestClient.Abstractions;
using Magento.RestClient.Context;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Tests.Constants;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
	public abstract class AbstractIntegrationTest
    {
        protected IAdminContext Context;

        [SetUp]
        public void Setup()
        {
	        Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

			var conf = TestConfiguration.GetInstance();

			
			this.Context = new MagentoAdminContext(conf);
		}
    }
}
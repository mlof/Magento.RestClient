using Magento.RestClient.Abstractions;
using Magento.RestClient.Context;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Tests.Domain.Constants;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests.Domain.Abstractions
{
	public abstract class AbstractDomainObjectTest
	{
		protected IAdminContext Context;

		[SetUp]
		public void Setup()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();
			var conf = TestConfiguration.GetInstance();

			this.Context = new MagentoAdminContext(conf);
		}

		[TearDown]
		public void Teardown()
		{
		}
	}
}
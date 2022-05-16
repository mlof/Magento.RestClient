using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Customers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
	public abstract class AbstractAdminTest
	{
		protected IAdminContext Context;

		[OneTimeSetUp]
		async public Task Setup()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

			var configurationRoot = new ConfigurationBuilder().AddUserSecrets<MagentoClientOptions>().Build();

			var configuration = new MagentoClientOptions();
			configurationRoot.Bind(configuration);

			this.Context = new MagentoAdminContext(configuration);

		}



		[TearDown]
		public void Teardown()
		{
		}
	}
}